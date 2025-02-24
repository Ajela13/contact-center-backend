using Microsoft.AspNetCore.Mvc;
using ContactCenterAPI.Models;
using ContactCenterAPI.Services.Interfaces;
using System.Collections.Generic;


namespace ContactCenterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Client>> GetClients()
        {
            var clients = _clientService.GetClients();
            return Ok(clients);
        }
    }
}
