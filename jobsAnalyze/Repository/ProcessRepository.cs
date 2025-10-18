using jobsAnalyze.Database;
using Microsoft.EntityFrameworkCore;
using jobsAnalyze.Models.DTO;

namespace jobsAnalyze.Repository
{
    public class ProcessRepository
    {
        private ApplicationDbContext _context;
        public ProcessRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<List<Process>> GetProcessByUserId(string userId) 
        {
            return await _context
                            .Processes
                            .Where(x => x.UserId == userId)
                            .ToListAsync<Process>();
        }

        public async Task<bool> CreateProcess(Process process)
        {
            await _context.Processes.AddAsync(process);
            return await Save();
        }

        private async Task<bool> Save()
        {
            int recordsCount = await _context.SaveChangesAsync();
            return recordsCount > 0;
        }
    }
}
