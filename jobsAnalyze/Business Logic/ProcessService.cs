using AutoMapper;
using jobsAnalyze.Business_Logic.Interfaces;
using jobsAnalyze.Database;
using jobsAnalyze.Models.DTO;
using jobsAnalyze.Models.Process.BE;
using Microsoft.EntityFrameworkCore;

namespace jobsAnalyze.Business_Logic
{
    public class ProcessService : IProcessService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProcessService(ApplicationDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Process>> GetProcesses(string userId)
        {
            return await _context.Processes.Where(x => x.UserId == userId).ToListAsync<Process>();
        }

        public async Task<bool> CreateProcess(CreateProcessBE process)
        {
            try
            {
                Process processDto = _mapper.Map<Process>(process);
                await _context.Processes.AddAsync(processDto);
                int resCount = await _context.SaveChangesAsync();
                return resCount > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
