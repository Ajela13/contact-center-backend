using ContactCenterAPI.Models;
using System.Collections.Generic;


namespace ContactCenterAPI.Repositories.Interfaces
{
    public interface IAgentRepository
    {
        IEnumerable<Agent> GetAllAgents();
    }
}
