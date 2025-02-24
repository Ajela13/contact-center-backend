namespace ContactCenterAPI.Models
{
    public class Agent
    {
        public int Id { get; set; }  // Identificador (útil para persistencia)
        public string Name { get; set; }
        public string State { get; set; }  // "available", "busy", "paused"
        public int WaitTime { get; set; }
    }
}
