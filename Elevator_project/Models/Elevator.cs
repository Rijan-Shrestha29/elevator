using System;
using System.Windows.Forms;

namespace Elevator_project.Models
{
    public class Elevator
    {
        public int CurrentFloor { get; private set; }
        public bool IsMoving { get; private set; }
        public event EventHandler<int> FloorChanged;
        public event EventHandler MovementCompleted;

        public void MoveToFloor(int targetFloor)
        {
            if (IsMoving || targetFloor == CurrentFloor)
                return;

            IsMoving = true;
        }

        public void OnArrivedAtFloor(int floor)
        {
            CurrentFloor = floor;
            IsMoving = false;
            FloorChanged?.Invoke(this, floor);
            MovementCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}