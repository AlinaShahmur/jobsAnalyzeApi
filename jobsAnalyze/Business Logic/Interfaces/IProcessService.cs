using jobsAnalyze.Models.DTO;
using jobsAnalyze.Models.Process.BE;

namespace jobsAnalyze.Business_Logic.Interfaces
{
    public interface IProcessService
    {
        public Task<List<Process>> GetProcesses(string userId);
        public Task<bool> CreateProcess(CreateProcessBE newProcess);
    }
}
