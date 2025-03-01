using ContactCenterAPI.Models;
using ContactCenterAPI.Repositories.Interfaces;
using ContactCenterAPI.Services.Interfaces;
using System.Collections.Generic;

namespace ContactCenterAPI.Services.Implementations
{
    public class AgentService : IAgentService
    {
        private readonly IAgentRepository _agentRepository;
        public AgentService(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        public IEnumerable<Agent> GetAgents(string? state = null)
        {
            return _agentRepository.GetAllAgents();
        }
    }
}
