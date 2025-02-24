namespace ContactCenterAPI.Models
{
    public class Agent
    {
        public required int Id { get; set; }  // Identificador (útil para persistencia)
        public required string Name { get; set; }
        public required string State { get; set; }  // "available", "busy", "paused"
        public required int WaitTime { get; set; }
    }
}
