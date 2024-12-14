using AutoMapper;
using jobsAnalyze.Business_Logic;
using jobsAnalyze.Business_Logic.Interfaces;
using jobsAnalyze.Helpers.Interfaces;
using jobsAnalyze.Models;
using jobsAnalyze.Models.Application.BE;
using jobsAnalyze.Models.Application.Requests;
using jobsAnalyze.Models.DTO;
using jobsAnalyze.ResponseWrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace jobsAnalyze.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;
        private IFilesUtils _filesUtils;
        public ApplicationController(IApplicationService applicationService, IMapper mapper, IFilesUtils filesUtils)
        {
            _applicationService = applicationService;
            _mapper = mapper;
            _filesUtils = filesUtils;
        }

        [HttpGet]
        [Authorize]
        public async Task<List<Application>> GetApplicationsPaginated(int processId, [FromQuery] int pageNum, [FromQuery] int pageSize, [FromQuery] string sortBy, [FromQuery] int sortType)
        {
            return await _applicationService.GetPaginatedApplications(processId, pageNum, pageSize, sortBy, sortType);
        }

        [HttpPost]
        [Authorize]
        public async Task<BaseResponse> CreateApplication([FromBody] CreateApplicationRequest applicationBody)
        {
            CreateApplicationBE applicationBE = _mapper.Map<CreateApplicationBE>(applicationBody);
            ResponseBE res = await _applicationService.CreateApplication(applicationBE);
            return _mapper.Map<BaseResponse>(res);
        }
        [HttpGet]
        [Authorize]
        public async Task<FileResult> ExportApplications(int ProcessId)
        {

            List<Application> applications = await _applicationService.GetApplications(ProcessId);
            MemoryStream stream =  _filesUtils.CreateCSV(applications);
            return File(stream.ToArray(), "text/csv", "test");
        }
    }
}
