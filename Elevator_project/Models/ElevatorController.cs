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

        // Floor positions
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

        public ElevatorController(Panel panel, Label display, Logger log)
        {
            elevator = new Elevator();
            elevatorPanel = panel;
            displayLabel = display;
            logger = log;

            // Calculate floor positions
            floor0Y = elevatorPanel.Parent.Height - elevatorPanel.Height - 20;
            floor1Y = 20;

            // Resize elevator to fit properly in shaft
            ResizeElevatorToFitShaft();

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

            InitializeDoors();
        }

        private void ResizeElevatorToFitShaft()
        {
            if (elevatorPanel.Parent is Panel shaftPanel)
            {
                int shaftWidth = shaftPanel.Width;

                elevatorPanel.Width = 160;
                elevatorPanel.Height = 170;

                elevatorPanel.Left = (shaftWidth - elevatorPanel.Width) / 2;
                elevatorPanel.Top = (elevator.CurrentFloor == 0) ? floor0Y : floor1Y;
            }
        }

        private void InitializeDoors()
        {
            elevatorDoorLeft = elevatorPanel.Controls["elevatorDoorLeft"] as Panel;
            elevatorDoorRight = elevatorPanel.Controls["elevatorDoorRight"] as Panel;

            if (elevatorDoorLeft != null && elevatorDoorRight != null)
            {
                int doorWidth = elevatorPanel.Width / 2;
                int doorHeight = elevatorPanel.Height;

                elevatorDoorLeft.Size = new System.Drawing.Size(doorWidth, doorHeight);
                elevatorDoorRight.Size = new System.Drawing.Size(doorWidth, doorHeight);
                elevatorDoorRight.Location = new System.Drawing.Point(doorWidth, 0);
            }

            if (elevatorPanel.Parent is Panel shaftPanel)
            {
                if (shaftPanel.Controls["pnlFloor0Doors"] is Panel floor0Container)
                {
                    floor0Container.Size = new System.Drawing.Size(elevatorPanel.Width, elevatorPanel.Height);
                    floor0Container.Location = new System.Drawing.Point(elevatorPanel.Left, floor0Y);

                    floor0DoorLeft = CreateFloorDoor(floor0Container, "floor0DoorLeft", 0);
                    floor0DoorRight = CreateFloorDoor(floor0Container, "floor0DoorRight", elevatorPanel.Width / 2);
                }

                if (shaftPanel.Controls["pnlFloor1Doors"] is Panel floor1Container)
                {
                    floor1Container.Size = new System.Drawing.Size(elevatorPanel.Width, elevatorPanel.Height);
                    floor1Container.Location = new System.Drawing.Point(elevatorPanel.Left, floor1Y);

                    floor1DoorLeft = CreateFloorDoor(floor1Container, "floor1DoorLeft", 0);
                    floor1DoorRight = CreateFloorDoor(floor1Container, "floor1DoorRight", elevatorPanel.Width / 2);
                }
            }

            UpdateAllDoorPositions();
        }

        private static Panel CreateFloorDoor(Panel container, string name, int xPos)
        {
            int doorWidth = container.Width / 2;
            var door = new Panel
            {
                Name = name,
                Size = new System.Drawing.Size(doorWidth, container.Height),
                Location = new System.Drawing.Point(xPos, 0)
            };
            container.Controls.Add(door);
            door.BringToFront();
            return door;
        }

        public void GoToFloor(int floor)
        {
            if (elevator.IsMoving)
            {
                logger.Log($"Elevator is currently moving — request to floor {floor} ignored.");
                return;
            }

            logger.Log($"Request: Move to floor {floor}");
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

            logger.Log($"Moving from floor {elevator.CurrentFloor} to floor {targetFloor}");
            displayLabel.Text = $"Moving to {targetFloor}";

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

            logger.Log($"Opening doors at Floor {elevator.CurrentFloor}");

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

            logger.Log($"Closing doors at Floor {elevator.CurrentFloor}");

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
                logger.Log($"Auto-closing doors after 3 seconds at Floor {elevator.CurrentFloor}");
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
                logger.Log($"Doors opened at Floor {elevator.CurrentFloor}");
                displayLabel.Text = $"Floor {elevator.CurrentFloor} - Open";

                autoCloseTimer.Start();
                logger.Log($"Auto-close timer started (3 seconds) at Floor {elevator.CurrentFloor}");
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
                logger.Log($"Doors closed at Floor {elevator.CurrentFloor}");
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
                elevatorDoorRight.Location = new System.Drawing.Point(elevatorPanel.Width / 2 + doorOpenWidth, 0);
        }

        private void UpdateFloorDoorPositions()
        {
            if (floor0DoorLeft != null)
                floor0DoorLeft.Location = new System.Drawing.Point(-floor0DoorOpenWidth, 0);
            if (floor0DoorRight != null)
                floor0DoorRight.Location = new System.Drawing.Point(elevatorPanel.Width / 2 + floor0DoorOpenWidth, 0);

            if (floor1DoorLeft != null)
                floor1DoorLeft.Location = new System.Drawing.Point(-floor1DoorOpenWidth, 0);
            if (floor1DoorRight != null)
                floor1DoorRight.Location = new System.Drawing.Point(elevatorPanel.Width / 2 + floor1DoorOpenWidth, 0);
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
        }

        private void OnMovementCompleted(object sender, EventArgs e)
        {
            logger.Log($"Elevator arrived at Floor {elevator.CurrentFloor}");
            displayLabel.Text = $"Floor {elevator.CurrentFloor}";
            OpenDoors();
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