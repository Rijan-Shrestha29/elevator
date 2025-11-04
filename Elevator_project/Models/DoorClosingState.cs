namespace Elevator_project.Models
{
    public class DoorClosingState : ElevatorState
    {
        public DoorClosingState(ElevatorController controller) : base(controller) { }

        public override void MoveToFloor(int floor)
        {
            controller.Logger.Log("Cannot move while doors are closing", "WARNING");
        }

        public override void OpenDoors()
        {
            // Transition back to door opening state
            controller.SetState(new DoorOpeningState(controller));
            controller.OpenDoorsInternal();
        }

        public override void CloseDoors()
        {
            controller.Logger.Log("Doors are already closing", "INFO");
        }

        public override void ArriveAtFloor(int floor)
        {
            // Should not arrive at floor while closing doors
        }

        public override string GetStateName() => "DOOR_CLOSING";
    }
}