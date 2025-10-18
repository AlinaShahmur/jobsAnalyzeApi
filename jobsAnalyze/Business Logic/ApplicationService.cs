using AutoMapper;
using jobsAnalyze.Business_Logic.Interfaces;
using jobsAnalyze.Database;
using jobsAnalyze.Models;
using jobsAnalyze.Models.Application.BE;
using jobsAnalyze.Models.DTO;
using jobsAnalyze.Repository.Interfaces;
using jobsAnalyze.ResponseWrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jobsAnalyze.Helpers;

namespace jobsAnalyze.Business_Logic
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;

        public ApplicationService(IApplicationRepository applicationRepository, IMapper mapper)
        {
            _mapper = mapper;
            _applicationRepository = applicationRepository;
        }
        public async Task<List<Application>> GetApplications(int processId)
        {
             return await 
                       _applicationRepository
                       .GetApplicationsByProcessId(processId);
        }

        public async Task<List<Application>> GetPaginatedApplications(int processId, int pageNum, int pageSize, string sortBy= "StartDate", Enums.SortType sortType= Enums.SortType.ASC)
        {
            return await 
                    _applicationRepository
                    .GetApplicationsByProcessId(processId, pageNum, pageSize, sortBy, (int)sortType);
        }

        public async Task<ResponseBE> CreateApplication(CreateApplicationBE applicationBE) 
        {

            Application applicationDto = _mapper.Map<Application>(applicationBE);
            bool res = await _applicationRepository.CreateApplication(applicationDto);
            return res ? new ResponseBE(201) : new ResponseBE(500);
        }

    }
}
