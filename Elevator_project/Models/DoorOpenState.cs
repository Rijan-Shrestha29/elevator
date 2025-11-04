namespace Elevator_project.Models
{
    public class DoorOpenState : ElevatorState
    {
        public DoorOpenState(ElevatorController controller) : base(controller) { }

        public override void MoveToFloor(int floor)
        {
            // Close doors first, then move
            CloseDoors();
            controller.QueueFloorRequest(floor);
        }

        public override void OpenDoors()
        {
            controller.Logger.Log("Doors are already open", "INFO");
        }

        public override void CloseDoors()
        {
            // Transition to door closing state
            controller.SetState(new DoorClosingState(controller));
            controller.CloseDoorsInternal();
        }

        public override void ArriveAtFloor(int floor)
        {
            // Should not arrive at floor while doors are open
        }

        public override string GetStateName() => "DOOR_OPEN";
    }
}