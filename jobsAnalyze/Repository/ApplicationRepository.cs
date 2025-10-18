using jobsAnalyze.Database;
using jobsAnalyze.Models.Application.BE;
using jobsAnalyze.Models.DTO;
using jobsAnalyze.ResponseWrapper;
using Microsoft.EntityFrameworkCore;

namespace jobsAnalyze.Repository
{
    public class ApplicationRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Application>> GetApplicationsByProcessId(int processId)
        {
            return await _context
                            .Applications
                            .Where(a => a.ProcessId == processId)
                            .ToListAsync<Application>();
        }

        public async Task<List<Application>> GetApplicationsByProcessId(int processId, int pageNum, int pageSize, string sortBy = "StartDate", int sortType = 1)
        {
            return await _context
                .Applications
                .Where(a => a.ProcessId == processId)
                .OrderBy(x => EF.Property<object>(x, sortBy))
                .Skip((int)pageNum * (int)pageSize)
                .Take((int)pageSize)
                .ToListAsync<Application>();
        }

        public async Task<bool> CreateApplication(Application application)
        {
            await _context.Applications.AddAsync(application);
            return await Save();
        }

        private async Task<bool> Save()
        {
            int recordsCount = await _context.SaveChangesAsync();
            return recordsCount > 0;
        }
    }
}
