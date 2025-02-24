using ContactCenterAPI.Models;
using ContactCenterAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ContactCenterAPI.Repositories.Implementations
{
    public class AgentRepository : IAgentRepository
    {
        public IEnumerable<Agent> GetAllAgents()
        {
            var agentsData = WebSocketHandler.GetLatestAgentsData();
            return agentsData.Select(a => new Agent
            {
                Id = (int)a.GetType().GetProperty("id")?.GetValue(a),
                Name = (string)a.GetType().GetProperty("name")?.GetValue(a),
                State = (string)a.GetType().GetProperty("status")?.GetValue(a),
                WaitTime = (int)a.GetType().GetProperty("waitTime")?.GetValue(a)
            }).ToList();
        }
    }
}
