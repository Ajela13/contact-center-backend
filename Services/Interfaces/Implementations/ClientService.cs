using ContactCenterAPI.Models;
using ContactCenterAPI.Repositories.Interfaces;
using ContactCenterAPI.Services.Interfaces;
using System.Collections.Generic;


namespace ContactCenterAPI.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public IEnumerable<Client> GetClients(int? maxWaitTime = null)
        {
            return _clientRepository.GetAllClients();
        }
    }
}
