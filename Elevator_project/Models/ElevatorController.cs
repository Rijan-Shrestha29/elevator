using System;
using System.Windows.Forms;
using Elevator_project.Utils;
using Timer = System.Windows.Forms.Timer;

namespace Elevator_project.Models
{
    public class ElevatorController : IDisposable
    {
        // Core components
        private readonly Elevator elevator;
        private readonly Panel elevatorPanel;
        private readonly Label displayLabel;
        private readonly Timer moveTimer;
        private readonly Logger logger;

        // State management
        private ElevatorState currentState;
        private int? queuedFloorRequest;

        // Movement variables
        private int targetFloor;
        private int targetY;
        private readonly int floor0Y = 340; // Floor 0 position
        private readonly int floor1Y = 60;  // Floor 1 position

        // Door components
        private Panel elevatorDoorLeft;
        private Panel elevatorDoorRight;
        private Panel floor0DoorLeft;
        private Panel floor0DoorRight;
        private Panel floor1DoorLeft;
        private Panel floor1DoorRight;

        // Door animation variables
        private readonly Timer doorTimer;
        private readonly Timer floorDoorTimer;
        private readonly Timer autoCloseTimer;
        private int doorOpenWidth;
        private int floor0DoorOpenWidth;
        private int floor1DoorOpenWidth;
        private const int MAX_DOOR_OPEN = 85;
        private const int DOOR_SPEED = 5;
        private bool isOpeningDoors;
        private bool isClosingDoors;
        private bool doorsOpen;
        private bool openingElevatorDoors;
        private bool openingFloorDoors;

        // Public property for state classes to access logger
        public Logger Logger => logger;

        public ElevatorController(Panel panel, Label display, Logger log, Panel floor0DoorsContainer, Panel floor1DoorsContainer)
        {
            elevator = new Elevator();
            elevatorPanel = panel;
            displayLabel = display;
            logger = log;

            // Initialize with Idle state
            currentState = new IdleState(this);

            // Setup door references
            InitializeDoors(floor0DoorsContainer, floor1DoorsContainer);

            // Initialize timers
            moveTimer = new Timer { Interval = 30 };
            moveTimer.Tick += MoveElevatorTick;

            doorTimer = new Timer { Interval = 20 };
            doorTimer.Tick += DoorAnimationTick;

            floorDoorTimer = new Timer { Interval = 20 };
            floorDoorTimer.Tick += FloorDoorAnimationTick;

            autoCloseTimer = new Timer { Interval = 3000 };
            autoCloseTimer.Tick += AutoCloseDoors;

            // Subscribe to elevator events
            elevator.FloorChanged += OnFloorChanged;
            elevator.MovementCompleted += OnMovementCompleted;

            // Set initial position to floor 0
            elevatorPanel.Top = floor0Y;
            elevatorPanel.Visible = true;
            elevatorPanel.BringToFront();

            // Reset doors to closed state
            ResetAllDoors();

            logger.Log("Elevator system initialized at Floor 0", "SYSTEM");
            logger.Log($"State: {currentState.GetStateName()}", "STATE");
        }

        // State management method
        public void SetState(ElevatorState newState)
        {
            var oldState = currentState.GetStateName();
            currentState = newState;
            logger.Log($"State changed: {oldState} -> {newState.GetStateName()}", "STATE");
        }

        // Queue floor request for processing after doors close
        public void QueueFloorRequest(int floor)
        {
            queuedFloorRequest = floor;
        }

        // Public interface methods - delegate to current state
        public void GoToFloor(int floor)
        {
            currentState.MoveToFloor(floor);
        }

        public void ManualOpenDoors()
        {
            currentState.OpenDoors();
        }

        public void ManualCloseDoors()
        {
            currentState.CloseDoors();
        }

        // Internal methods called by state classes
        public void MoveToFloorInternal(int floor)
        {
            if (elevator.IsMoving) return;

            logger.Log($"Request to move to floor {floor}", "INFO");
            targetFloor = floor;

            if (elevator.CurrentFloor == targetFloor)
            {
                if (!doorsOpen)
                {
                    OpenDoorsInternal();
                }
                return;
            }

            StartMovement();
        }

        public void OpenDoorsInternal()
        {
            if (isOpeningDoors || doorsOpen) return;

            isOpeningDoors = true;
            isClosingDoors = false;
            autoCloseTimer.Stop();
            openingElevatorDoors = true;
            doorTimer.Start();

            logger.Log($"Opening doors at Floor {elevator.CurrentFloor}", "DOOR");

            // Start floor door animation with small delay
            var delayTimer = new Timer { Interval = 5 };
            delayTimer.Tick += (s, e) =>
            {
                openingFloorDoors = true;
                floorDoorTimer.Start();
                delayTimer.Stop();
                delayTimer.Dispose();
            };
            delayTimer.Start();
        }

        public void CloseDoorsInternal()
        {
            if (isClosingDoors || !doorsOpen) return;

            isClosingDoors = true;
            isOpeningDoors = false;
            autoCloseTimer.Stop();
            openingElevatorDoors = false;
            openingFloorDoors = false;
            doorTimer.Start();
            floorDoorTimer.Start();

            logger.Log($"Closing doors at Floor {elevator.CurrentFloor}", "DOOR");
        }

        public void ArriveAtFloorInternal(int floor)
        {
            OpenDoorsInternal();
        }

        // Initialize door panel references
        private void InitializeDoors(Panel floor0DoorsContainer, Panel floor1DoorsContainer)
        {
            // Get elevator doors
            elevatorDoorLeft = elevatorPanel.Controls["elevatorDoorLeft"] as Panel;
            elevatorDoorRight = elevatorPanel.Controls["elevatorDoorRight"] as Panel;

            // Get floor 0 doors
            if (floor0DoorsContainer != null)
            {
                floor0DoorLeft = floor0DoorsContainer.Controls["floor0DoorLeft"] as Panel;
                floor0DoorRight = floor0DoorsContainer.Controls["floor0DoorRight"] as Panel;
            }

            // Get floor 1 doors
            if (floor1DoorsContainer != null)
            {
                floor1DoorLeft = floor1DoorsContainer.Controls["floor1DoorLeft"] as Panel;
                floor1DoorRight = floor1DoorsContainer.Controls["floor1DoorRight"] as Panel;
            }

            EnsureDoorSizing();
        }

        // Ensure all doors have correct dimensions
        private void EnsureDoorSizing()
        {
            int doorWidth = 80;
            int doorHeight = 170;

            if (elevatorDoorLeft != null)
            {
                elevatorDoorLeft.Size = new System.Drawing.Size(doorWidth, doorHeight);
                elevatorDoorRight.Size = new System.Drawing.Size(doorWidth, doorHeight);
                elevatorDoorLeft.Visible = true;
                elevatorDoorRight.Visible = true;
            }

            if (floor0DoorLeft != null)
            {
                floor0DoorLeft.Size = new System.Drawing.Size(doorWidth, doorHeight);
                floor0DoorRight.Size = new System.Drawing.Size(doorWidth, doorHeight);
            }

            if (floor1DoorLeft != null)
            {
                floor1DoorLeft.Size = new System.Drawing.Size(doorWidth, doorHeight);
                floor1DoorRight.Size = new System.Drawing.Size(doorWidth, doorHeight);
            }
        }

        // Reset all doors to closed position
        private void ResetAllDoors()
        {
            doorOpenWidth = 0;
            floor0DoorOpenWidth = 0;
            floor1DoorOpenWidth = 0;
            UpdateAllDoorPositions();
        }

        // Start elevator movement to target floor
        private void StartMovement()
        {
            autoCloseTimer.Stop();
            targetY = (targetFloor == 0) ? floor0Y : floor1Y;

            logger.Log($"Moving from floor {elevator.CurrentFloor} to floor {targetFloor}", "MOVEMENT");
            displayLabel.Text = $"Moving to {targetFloor}";

            elevator.MoveToFloor(targetFloor);
            moveTimer.Start();
        }

        // Elevator movement animation
        private void MoveElevatorTick(object sender, EventArgs e)
        {
            int step = (targetY < elevatorPanel.Top) ? -5 : 5;
            elevatorPanel.Top += step;

            bool reachedTarget = (step > 0 && elevatorPanel.Top >= targetY) ||
                               (step < 0 && elevatorPanel.Top <= targetY);

            if (reachedTarget)
            {
                moveTimer.Stop();
                elevatorPanel.Top = targetY;
                elevator.OnArrivedAtFloor(targetFloor);
            }
        }

        // Door animation for elevator doors
        private void DoorAnimationTick(object sender, EventArgs e)
        {
            if (isOpeningDoors && openingElevatorDoors)
            {
                AnimateElevatorDoorsOpening();
            }
            else if (isClosingDoors)
            {
                AnimateElevatorDoorsClosing();
            }
        }

        // Door animation for floor doors
        private void FloorDoorAnimationTick(object sender, EventArgs e)
        {
            if (isOpeningDoors && openingFloorDoors)
            {
                AnimateFloorDoorsOpening();
            }
            else if (isClosingDoors)
            {
                AnimateFloorDoorsClosing();
            }
        }

        // Animate elevator doors opening
        private void AnimateElevatorDoorsOpening()
        {
            if (doorOpenWidth < MAX_DOOR_OPEN)
            {
                doorOpenWidth += DOOR_SPEED;
                if (doorOpenWidth > MAX_DOOR_OPEN)
                    doorOpenWidth = MAX_DOOR_OPEN;
            }
            else
            {
                openingElevatorDoors = false;
            }
            UpdateElevatorDoorPositions();
        }

        // Animate floor doors opening
        private void AnimateFloorDoorsOpening()
        {
            bool allDoorsOpen = true;

            if (elevator.CurrentFloor == 0 && floor0DoorOpenWidth < MAX_DOOR_OPEN)
            {
                floor0DoorOpenWidth += DOOR_SPEED;
                if (floor0DoorOpenWidth > MAX_DOOR_OPEN)
                    floor0DoorOpenWidth = MAX_DOOR_OPEN;
                allDoorsOpen = false;
            }
            else if (elevator.CurrentFloor == 1 && floor1DoorOpenWidth < MAX_DOOR_OPEN)
            {
                floor1DoorOpenWidth += DOOR_SPEED;
                if (floor1DoorOpenWidth > MAX_DOOR_OPEN)
                    floor1DoorOpenWidth = MAX_DOOR_OPEN;
                allDoorsOpen = false;
            }

            UpdateFloorDoorPositions();

            if (allDoorsOpen && doorOpenWidth >= MAX_DOOR_OPEN)
            {
                doorTimer.Stop();
                floorDoorTimer.Stop();
                isOpeningDoors = false;
                doorsOpen = true;
                logger.Log($"Doors fully opened at Floor {elevator.CurrentFloor}", "DOOR");
                displayLabel.Text = $"Floor {elevator.CurrentFloor} - Open";

                // Transition to DoorOpen state
                SetState(new DoorOpenState(this));

                autoCloseTimer.Start();
            }
        }

        // Animate elevator doors closing
        private void AnimateElevatorDoorsClosing()
        {
            if (doorOpenWidth > 0)
            {
                doorOpenWidth -= DOOR_SPEED;
                if (doorOpenWidth < 0)
                    doorOpenWidth = 0;
            }
            UpdateElevatorDoorPositions();
        }

        // Animate floor doors closing
        private void AnimateFloorDoorsClosing()
        {
            bool allDoorsClosed = true;

            if (elevator.CurrentFloor == 0 && floor0DoorOpenWidth > 0)
            {
                floor0DoorOpenWidth -= DOOR_SPEED;
                if (floor0DoorOpenWidth < 0)
                    floor0DoorOpenWidth = 0;
                allDoorsClosed = false;
            }
            else if (elevator.CurrentFloor == 1 && floor1DoorOpenWidth > 0)
            {
                floor1DoorOpenWidth -= DOOR_SPEED;
                if (floor1DoorOpenWidth < 0)
                    floor1DoorOpenWidth = 0;
                allDoorsClosed = false;
            }

            UpdateFloorDoorPositions();

            if (allDoorsClosed && doorOpenWidth <= 0)
            {
                doorTimer.Stop();
                floorDoorTimer.Stop();
                isClosingDoors = false;
                doorsOpen = false;
                logger.Log($"Doors fully closed at Floor {elevator.CurrentFloor}", "DOOR");
                displayLabel.Text = $"Floor {elevator.CurrentFloor}";

                // Transition to Idle state
                SetState(new IdleState(this));

                autoCloseTimer.Stop();

                // Process any queued floor request
                if (queuedFloorRequest.HasValue)
                {
                    int floor = queuedFloorRequest.Value;
                    queuedFloorRequest = null;
                    GoToFloor(floor);
                }
            }
        }

        // Update elevator door positions
        private void UpdateElevatorDoorPositions()
        {
            if (elevatorDoorLeft != null)
                elevatorDoorLeft.Location = new System.Drawing.Point(-doorOpenWidth, 0);
            if (elevatorDoorRight != null)
                elevatorDoorRight.Location = new System.Drawing.Point(elevatorDoorLeft.Width + doorOpenWidth, 0);
        }

        // Update floor door positions
        private void UpdateFloorDoorPositions()
        {
            // Floor 0 doors
            if (floor0DoorLeft != null)
                floor0DoorLeft.Location = new System.Drawing.Point(-floor0DoorOpenWidth, 0);
            if (floor0DoorRight != null)
                floor0DoorRight.Location = new System.Drawing.Point(floor0DoorLeft.Width + floor0DoorOpenWidth, 0);

            // Floor 1 doors
            if (floor1DoorLeft != null)
                floor1DoorLeft.Location = new System.Drawing.Point(-floor1DoorOpenWidth, 0);
            if (floor1DoorRight != null)
                floor1DoorRight.Location = new System.Drawing.Point(floor1DoorLeft.Width + floor1DoorOpenWidth, 0);
        }

        // Update all door positions
        private void UpdateAllDoorPositions()
        {
            UpdateElevatorDoorPositions();
            UpdateFloorDoorPositions();
        }

        // Auto-close doors after timeout
        private void AutoCloseDoors(object sender, EventArgs e)
        {
            autoCloseTimer.Stop();

            if (doorsOpen && !isClosingDoors && !elevator.IsMoving)
            {
                logger.Log($"Auto-closing doors after 3 seconds at Floor {elevator.CurrentFloor}", "DOOR");
                CloseDoorsInternal();
            }
        }

        // Handle floor change event
        private void OnFloorChanged(object sender, int floor)
        {
            displayLabel.Text = $"Floor {floor}";
            logger.Log($"Reached floor {floor}", "ARRIVAL");
        }

        // Handle movement completion event
        private void OnMovementCompleted(object sender, EventArgs e)
        {
            logger.Log($"Elevator arrived at Floor {elevator.CurrentFloor}", "ARRIVAL");
            displayLabel.Text = $"Floor {elevator.CurrentFloor}";
            currentState.ArriveAtFloor(elevator.CurrentFloor);
        }

        // Clean up resources
        public void Dispose()
        {
            moveTimer?.Dispose();
            doorTimer?.Dispose();
            floorDoorTimer?.Dispose();
            autoCloseTimer?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}