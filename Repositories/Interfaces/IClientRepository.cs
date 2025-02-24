using ContactCenterAPI.Models;
using System.Collections.Generic;

namespace ContactCenterAPI.Repositories.Interfaces
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
    }
}
