namespace Elevator_project.Models
{
    public class MovingState : ElevatorState
    {
        public MovingState(ElevatorController controller) : base(controller) { }

        public override void MoveToFloor(int floor)
        {
            controller.Logger.Log("Elevator is already moving - ignoring new request", "WARNING");
        }

        public override void OpenDoors()
        {
            controller.Logger.Log("Cannot open doors while moving", "WARNING");
        }

        public override void CloseDoors()
        {
            controller.Logger.Log("Doors are already closed while moving", "INFO");
        }

        public override void ArriveAtFloor(int floor)
        {
            // Transition to door opening state after arrival
            controller.SetState(new DoorOpeningState(controller));
            controller.ArriveAtFloorInternal(floor);
        }

        public override string GetStateName() => "MOVING";
    }
}