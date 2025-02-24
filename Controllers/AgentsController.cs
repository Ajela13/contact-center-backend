using Microsoft.AspNetCore.Mvc;
using ContactCenterAPI.Models;
using ContactCenterAPI.Services.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ContactCenterAPI.Repositories.Interfaces;


namespace ContactCenterAPI.Controllers
{
    [ApiController]
    [Route("/agents")]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentRepository _agentRepository;

        public AgentsController(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        [HttpGet]
        public IActionResult GetAgents([FromQuery] string? state)
        {
            var agents = _agentRepository.GetAllAgents(state);
            return Ok(agents);
        }
    }
}
