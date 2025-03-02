using Microsoft.AspNetCore.Mvc;
using ContactCenterAPI.Models;
using ContactCenterAPI.Services.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ContactCenterAPI.Controllers
{
    [ApiController]
    [Route("/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult GetClients([FromQuery] int? maxWaitTime)
        {
            var clients = _clientService.GetClients(maxWaitTime);
            return Ok(clients);
        }
    }
}
