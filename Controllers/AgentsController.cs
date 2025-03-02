using Microsoft.AspNetCore.Mvc;
using ContactCenterAPI.Models;
using ContactCenterAPI.Services.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


namespace ContactCenterAPI.Controllers
{
    [ApiController]
    [Route("/agents")]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentsController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpGet]
        public IActionResult GetAgents([FromQuery] string? state)
        {
            var agents = _agentService.GetAgents(state);
            return Ok(agents);
        }
    }
}
