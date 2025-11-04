namespace Elevator_project.Models
{
    public class DoorOpeningState : ElevatorState
    {
        public DoorOpeningState(ElevatorController controller) : base(controller) { }

        public override void MoveToFloor(int floor)
        {
            controller.Logger.Log("Cannot move while doors are opening", "WARNING");
        }

        public override void OpenDoors()
        {
            controller.Logger.Log("Doors are already opening", "INFO");
        }

        public override void CloseDoors()
        {
            // Transition to door closing state
            controller.SetState(new DoorClosingState(controller));
            controller.CloseDoorsInternal();
        }

        public override void ArriveAtFloor(int floor)
        {
            // Should not arrive at floor while opening doors
        }

        public override string GetStateName() => "DOOR_OPENING";
    }
}