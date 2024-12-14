using jobsAnalyze.Models;
using jobsAnalyze.Models.Application.BE;
using jobsAnalyze.Models.DTO;
using jobsAnalyze.ResponseWrapper;
using Microsoft.AspNetCore.Mvc;

namespace jobsAnalyze.Business_Logic.Interfaces
{
    public interface IApplicationService
    {
        public Task<List<Application>> GetApplications(int processId);
        public Task<List<Application>> GetPaginatedApplications(int processId, int pageNum, int pageSize, string sortBy, int sortType);
        public Task<ResponseBE> CreateApplication(CreateApplicationBE application);
    }
}
