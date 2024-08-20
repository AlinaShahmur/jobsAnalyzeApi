using jobsAnalyze.Business_Logic.Interfaces;
using jobsAnalyze.Database;
using jobsAnalyze.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace jobsAnalyze.Business_Logic
{
    public class ProcessService : IProcessService
    {

        private readonly ApplicationDbContext _context;
        public ProcessService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<List<Process>> GetProcesses(string userId)
        {
            return await _context.Processes.Where(x => x.UserId == userId).ToListAsync<Process>();
        }
    }
}
