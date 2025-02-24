using Microsoft.AspNetCore.Mvc;
using ContactCenterAPI.Models;
using ContactCenterAPI.Services.Interfaces;
using System.Collections.Generic;


namespace ContactCenterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentService _agentService;
        public AgentsController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Agent>> GetAgents()
        {
            var agents = _agentService.GetAgents();
            return Ok(agents);
        }
    }
}
