using Microsoft.AspNetCore.Mvc;
using ContactCenterAPI.Models;
using ContactCenterAPI.Services.Interfaces;
using System.Collections.Generic;
using ContactCenterAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactCenterAPI.Controllers
{
    [ApiController]
    [Route("/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public IActionResult GetClients([FromQuery] int? maxWaitTime)
        {
            var clients = _clientRepository.GetAllClients(maxWaitTime);
            return Ok(clients);
        }
    }
}
