namespace Elevator_project.Models
{
    public class Floor
    {
        public int Number { get; }
        public bool HasRequest { get; set; }

        public Floor(int number)
        {
            Number = number;
            HasRequest = false;
        }
    }
}