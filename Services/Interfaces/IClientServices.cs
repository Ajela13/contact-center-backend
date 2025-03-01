using ContactCenterAPI.Models;
using System.Collections.Generic;


namespace ContactCenterAPI.Services.Interfaces
{
    public interface IClientService
    {
        IEnumerable<Client> GetClients(int? maxWaitTime = null);
    }
}
