using ContactCenterAPI.Models;
using System.Collections.Generic;


namespace ContactCenterAPI.Services.Interfaces
{
    public interface IAgentService
    {
        IEnumerable<Agent> GetAgents();
    }
}
