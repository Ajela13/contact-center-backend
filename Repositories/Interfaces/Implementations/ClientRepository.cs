using ContactCenterAPI.Models;
using ContactCenterAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ContactCenterAPI.Repositories.Implementations
{
    public class ClientRepository : IClientRepository
    {
        public IEnumerable<Client> GetAllClients(int? maxWaitTime = null)
        {
            var clientsData = WebSocketHandler.GetLatestClientsData(); // Función que obtenga clientes

            var clients = clientsData.Select(c => new Client
            {
                Id = (int)c.GetType().GetProperty("id")?.GetValue(c),
                Name = (string)c.GetType().GetProperty("name")?.GetValue(c),
                WaitTime = (int)c.GetType().GetProperty("waitTime")?.GetValue(c)
            });

            // Filtrar por tiempo de espera si se proporciona un valor
            if (maxWaitTime.HasValue)
            {
                clients = clients.Where(c => c.WaitTime <= maxWaitTime.Value);
            }

            return clients.ToList();
        }
    }
}
