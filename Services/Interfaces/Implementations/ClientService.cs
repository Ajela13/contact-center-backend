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
<<<<<<< HEAD
            return _clientRepository.GetAllClients(maxWaitTime);
=======
            return _clientRepository.GetAllClients(state);
>>>>>>> 51802d469f0644bba536a8d63904470453306b35
        }
    }
}
