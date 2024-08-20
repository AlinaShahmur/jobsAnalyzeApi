using jobsAnalyze.Business_Logic.Interfaces;
using jobsAnalyze.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jobsAnalyze.Controllers
{
    
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProcessController : ControllerBase
    {
        private readonly IProcessService _processService;
        public ProcessController(IProcessService processService) 
        {
            _processService = processService;
        }

        [HttpGet]
        [Authorize]
        public async Task<List<Process>> GetProcesses()
        {
            var userIdClaim = HttpContext.User.FindFirst("userId");
            if (userIdClaim != null)
            {
                string? userId = userIdClaim.Value;
                return await _processService.GetProcesses(userId);
            }
            return null;
        }

    }
}
