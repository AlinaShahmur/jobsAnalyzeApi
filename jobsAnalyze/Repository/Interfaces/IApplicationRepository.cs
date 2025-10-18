using jobsAnalyze.Models.Application.BE;
using jobsAnalyze.Models.DTO;
using jobsAnalyze.ResponseWrapper;

namespace jobsAnalyze.Repository.Interfaces
{
    public interface IApplicationRepository
    {
        Task<List<Application>> GetApplicationsByProcessId(int processId);
        Task<List<Application>> GetApplicationsByProcessId(int processId, int pageNum, int pageSize, string sortBy = "StartDate", int sortType = 1);
        Task<bool> CreateApplication(Application applicationBE);
    }
}
