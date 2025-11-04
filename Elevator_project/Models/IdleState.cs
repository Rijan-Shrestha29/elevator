namespace Elevator_project.Models
{
    public class IdleState : ElevatorState
    {
        public IdleState(ElevatorController controller) : base(controller) { }

        public override void MoveToFloor(int floor)
        {
            // Transition to moving state
            controller.SetState(new MovingState(controller));
            controller.MoveToFloorInternal(floor);
        }

        public override void OpenDoors()
        {
            // Transition to door opening state
            controller.SetState(new DoorOpeningState(controller));
            controller.OpenDoorsInternal();
        }

        public override void CloseDoors()
        {
            // Doors are already closed in idle state
            controller.Logger.Log("Doors are already closed", "INFO");
        }

        public override void ArriveAtFloor(int floor)
        {
            // Already at a floor, no action needed
        }

        public override string GetStateName() => "IDLE";
    }
}