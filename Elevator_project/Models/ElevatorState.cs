using System;

namespace Elevator_project.Models
{
    public abstract class ElevatorState
    {
        protected ElevatorController controller;

        public ElevatorState(ElevatorController controller)
        {
            this.controller = controller;
        }

        public abstract void MoveToFloor(int floor);
        public abstract void OpenDoors();
        public abstract void CloseDoors();
        public abstract void ArriveAtFloor(int floor);
        public abstract string GetStateName();
    }
}