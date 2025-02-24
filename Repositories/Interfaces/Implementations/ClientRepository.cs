using ContactCenterAPI.Models;
using ContactCenterAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ContactCenterAPI.Repositories.Implementations
{
    public class ClientRepository : IClientRepository
    {
        public IEnumerable<Client> GetAllClients()
        {
            var clientsData = WebSocketHandler.GetLatestClientsData();
            return clientsData.Select(c => new Client
            {
                Id = (int)c.GetType().GetProperty("id")?.GetValue(c),
                Name = (string)c.GetType().GetProperty("name")?.GetValue(c),
                WaitTime = (int)c.GetType().GetProperty("waitTime")?.GetValue(c)
            }).ToList();
        }
    }
}
