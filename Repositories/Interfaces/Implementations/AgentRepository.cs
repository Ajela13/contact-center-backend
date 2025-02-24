using ContactCenterAPI.Models;
using ContactCenterAPI.Repositories.Interfaces;


namespace ContactCenterAPI.Repositories.Implementations
{
    public class AgentRepository : IAgentRepository
    {
        // Datos simulados. En un escenario real se usaría acceso a base de datos.
        private readonly List<Agent> _agents = new List<Agent>
        {
            new Agent { Id = 1, Name = "Carlos", State = "available", WaitTime = 5 },
            new Agent { Id = 2, Name = "Ana", State = "busy", WaitTime = 10 },
            new Agent { Id = 3, Name = "Luis", State = "paused", WaitTime = 3 }
        };

        public IEnumerable<Agent> GetAllAgents() => _agents;
    }
}
