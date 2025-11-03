using System;
using System.Windows.Forms;
using Elevator_project.Utils;
using Timer = System.Windows.Forms.Timer;

namespace Elevator_project.Models
{
    public class ElevatorController : IDisposable
    {
        private readonly Elevator elevator;
        private readonly Panel elevatorPanel;
        private readonly Label displayLabel;
        private readonly Timer moveTimer;
        private readonly Logger logger;
        private int targetFloor;
        private int targetY;

        // Floor positions - CRITICAL: These must match the actual positions in the form
        private readonly int floor0Y;
        private readonly int floor1Y;

        // Door panels
        private Panel elevatorDoorLeft;
        private Panel elevatorDoorRight;
        private Panel floor0DoorLeft;
        private Panel floor0DoorRight;
        private Panel floor1DoorLeft;
        private Panel floor1DoorRight;

        // Door animation
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

        public ElevatorController(Panel panel, Label display, Logger log, Panel floor0DoorsContainer, Panel floor1DoorsContainer)
        {
            elevator = new Elevator();
            elevatorPanel = panel;
            displayLabel = display;
            logger = log;

            // CRITICAL FIX: Correct floor positions that match the designer
            floor0Y = 340; // pnlElevator.Location.Y for floor 0
            floor1Y = 60; // pnlElevator.Location.Y for floor 1 (80 + 60)

            // Initialize door references
            InitializeDoors(floor0DoorsContainer, floor1DoorsContainer);

            moveTimer = new Timer { Interval = 30 };
            moveTimer.Tick += MoveElevatorTick;

            doorTimer = new Timer { Interval = 20 };
            doorTimer.Tick += DoorAnimationTick;

            floorDoorTimer = new Timer { Interval = 20 };
            floorDoorTimer.Tick += FloorDoorAnimationTick;

            // Auto-close timer - 3 seconds
            autoCloseTimer = new Timer { Interval = 3000 };
            autoCloseTimer.Tick += AutoCloseDoors;

            elevator.FloorChanged += OnFloorChanged;
            elevator.MovementCompleted += OnMovementCompleted;

            // Set initial elevator position to floor 0
            elevatorPanel.Top = floor0Y;

            // Make sure elevator is visible
            elevatorPanel.Visible = true;
            elevatorPanel.BringToFront();

            // Initialize door positions to closed state
            ResetAllDoors();

            // Log initialization
            logger.Log("Elevator system initialized at Floor 0", "SYSTEM");
        }

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

            // Ensure all doors are properly sized and visible
            EnsureDoorSizing();
        }

        private void EnsureDoorSizing()
        {
            // All doors should be 80x170
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

        private void ResetAllDoors()
        {
            doorOpenWidth = 0;
            floor0DoorOpenWidth = 0;
            floor1DoorOpenWidth = 0;
            UpdateAllDoorPositions();
        }

        public void GoToFloor(int floor)
        {
            if (elevator.IsMoving)
            {
                logger.Log($"Elevator is currently moving — request to floor {floor} ignored.", "WARNING");
                return;
            }

            logger.Log($"Button pressed: Move to floor {floor}", "INFO");
            targetFloor = floor;

            if (elevator.CurrentFloor == targetFloor)
            {
                if (!doorsOpen)
                {
                    OpenDoors();
                }
                return;
            }

            if (doorsOpen)
            {
                CloseDoors(true);
            }
            else
            {
                StartMovement();
            }
        }

        private void StartMovement()
        {
            autoCloseTimer.Stop();

            targetY = (targetFloor == 0) ? floor0Y : floor1Y;

            logger.Log($"Moving from floor {elevator.CurrentFloor} to floor {targetFloor}", "MOVEMENT");
            displayLabel.Text = $"Moving to {targetFloor}";

            // Start elevator movement
            elevator.MoveToFloor(targetFloor);
            moveTimer.Start();
        }

        private void OpenDoors()
        {
            if (isOpeningDoors || doorsOpen) return;

            isOpeningDoors = true;
            isClosingDoors = false;
            autoCloseTimer.Stop();
            openingElevatorDoors = true;
            doorTimer.Start();

            logger.Log($"Opening doors at Floor {elevator.CurrentFloor}", "DOOR");

            // Start floor door animation with a small delay
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

        private void CloseDoors(bool moveAfterClosing = false)
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

            if (moveAfterClosing)
            {
                doorTimer.Tag = "move_after_close";
            }
        }

        private void AutoCloseDoors(object sender, EventArgs e)
        {
            autoCloseTimer.Stop();

            if (doorsOpen && !isClosingDoors && !elevator.IsMoving)
            {
                logger.Log($"Auto-closing doors after 3 seconds at Floor {elevator.CurrentFloor}", "DOOR");
                CloseDoors();
            }
        }

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

                // Start auto-close timer
                autoCloseTimer.Start();
            }
        }

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

                autoCloseTimer.Stop();

                if (doorTimer.Tag != null && doorTimer.Tag.ToString() == "move_after_close")
                {
                    doorTimer.Tag = null;
                    StartMovement();
                }
            }
        }

        private void UpdateElevatorDoorPositions()
        {
            if (elevatorDoorLeft != null)
                elevatorDoorLeft.Location = new System.Drawing.Point(-doorOpenWidth, 0);
            if (elevatorDoorRight != null)
                elevatorDoorRight.Location = new System.Drawing.Point(elevatorDoorLeft.Width + doorOpenWidth, 0);
        }

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

        private void UpdateAllDoorPositions()
        {
            UpdateElevatorDoorPositions();
            UpdateFloorDoorPositions();
        }

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

        private void OnFloorChanged(object sender, int floor)
        {
            displayLabel.Text = $"Floor {floor}";
            logger.Log($"Reached floor {floor}", "ARRIVAL");
        }

        private void OnMovementCompleted(object sender, EventArgs e)
        {
            logger.Log($"Elevator arrived at Floor {elevator.CurrentFloor}", "ARRIVAL");
            displayLabel.Text = $"Floor {elevator.CurrentFloor}";
            OpenDoors();
        }

        public void ManualOpenDoors()
        {
            if (!doorsOpen && !isOpeningDoors && !elevator.IsMoving)
            {
                OpenDoors();
            }
        }

        public void ManualCloseDoors()
        {
            if (doorsOpen && !isClosingDoors && !elevator.IsMoving)
            {
                CloseDoors();
            }
        }

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