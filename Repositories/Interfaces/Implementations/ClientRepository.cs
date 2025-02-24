using ContactCenterAPI.Models;
using ContactCenterAPI.Repositories.Interfaces;
using System.Collections.Generic;

namespace ContactCenterAPI.Repositories.Implementations
{
    public class ClientRepository : IClientRepository
    {
        // Datos simulados.
        private readonly List<Client> _clients = new List<Client>
        {
            new Client { Id = 1, Name = "María", WaitTime = 12 },
            new Client { Id = 2, Name = "Pedro", WaitTime = 8 }
        };

        public IEnumerable<Client> GetAllClients() => _clients;
    }
}
