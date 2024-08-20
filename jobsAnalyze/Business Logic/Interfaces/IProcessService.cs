using jobsAnalyze.Models.DTO;

namespace jobsAnalyze.Business_Logic.Interfaces
{
    public interface IProcessService
    {
        public Task<List<Process>> GetProcesses(string userId);
    }
}
