namespace ContactCenterAPI.Models
{
    public class Client
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int WaitTime { get; set; }
    }
}
