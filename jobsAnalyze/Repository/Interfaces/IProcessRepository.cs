using jobsAnalyze.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace jobsAnalyze.Repository.Interfaces
{
    public interface IProcessRepository
    {
        Task<List<Process>> GetProcessByUserId(string userId);
        Task<bool> CreateProcess(Process process);
        Task<bool> Save();
    }
}
