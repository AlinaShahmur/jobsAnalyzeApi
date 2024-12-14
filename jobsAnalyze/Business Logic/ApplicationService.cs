using AutoMapper;
using jobsAnalyze.Business_Logic.Interfaces;
using jobsAnalyze.Database;
using jobsAnalyze.Models;
using jobsAnalyze.Models.Application.BE;
using jobsAnalyze.Models.DTO;
using jobsAnalyze.ResponseWrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jobsAnalyze.Business_Logic
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ApplicationService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<Application>> GetApplications(int processId)
        {
            return await _context
                            .Applications
                            .Where(a => a.ProcessId == processId)
                            .ToListAsync<Application>();
        }


        public async Task<List<Application>> GetPaginatedApplications(int processId, int pageNum, int pageSize, string sortBy= "StartDate", int sortType=1)
        {
            return await _context
                            .Applications
                            .Where(a => a.ProcessId == processId)
                            .OrderBy(x => EF.Property<object>(x, sortBy))
                            .Skip((int)pageNum * (int)pageSize)
                            .Take((int)pageSize)
                            .ToListAsync<Application>(); 
        }



        public async Task<ResponseBE> CreateApplication(CreateApplicationBE applicationBE) 
        {

            Application applicationDto = _mapper.Map<Application>(applicationBE);
            await _context.Applications.AddAsync(applicationDto);
            int resCount = await _context.SaveChangesAsync();
            if (resCount > 0)
            {
                return new ResponseBE(201);
            }
            return new ResponseBE(500);
        }

    }
}
