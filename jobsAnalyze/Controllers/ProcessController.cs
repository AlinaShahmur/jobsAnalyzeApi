using AutoMapper;
using jobsAnalyze.Business_Logic.Interfaces;
using jobsAnalyze.Models.DTO;
using jobsAnalyze.Models.Process;
using jobsAnalyze.Models.Process.BE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jobsAnalyze.Controllers
{
    
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProcessController : ControllerBase
    {
        private readonly IProcessService _processService;
        private readonly IMapper _mapper;
        public ProcessController(IProcessService processService, IMapper mapper) 
        {
            _processService = processService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<List<Process>> GetProcesses()
        {
            string? userId = GetCurrentUserId();
            if (userId != null)
            {
                return await _processService.GetProcesses(userId);
            }
            return null;
        }

        [HttpPost]
        [Authorize]

        public async Task<bool> CreateProcess(CreateProcessRequest newProcess)
        {
            string? userId = GetCurrentUserId();
            if (userId != null)
            {
                CreateProcessBE processBE = _mapper.Map<CreateProcessBE>(newProcess, opt =>
                    {
                        opt.AfterMap((src, dest) => dest.UserId = userId);
                    }
                );
                return await _processService.CreateProcess(processBE);
            }
            return false;
        }

        private string? GetCurrentUserId()
        {
            var userIdClaim = HttpContext.User.FindFirst("userId");
            if (userIdClaim != null)
            {
                string? userId = userIdClaim.Value;
                return userId;
            }
            return null;
        }
    }
}
