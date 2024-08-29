namespace Calendar.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public required string Description { get; set; }
    }
}
