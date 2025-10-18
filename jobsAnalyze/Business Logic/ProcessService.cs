using AutoMapper;
using jobsAnalyze.Business_Logic.Interfaces;
using jobsAnalyze.Database;
using jobsAnalyze.Models.DTO;
using jobsAnalyze.Models.Process.BE;
using jobsAnalyze.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace jobsAnalyze.Business_Logic
{
    public class ProcessService : IProcessService
    {
        private readonly IMapper _mapper;
        private readonly IProcessRepository _processRepository;
        public ProcessService(IMapper mapper, IProcessRepository processRepository) 
        {
 
            _mapper = mapper;
            _processRepository = processRepository;
        }

        public async Task<List<Process>> GetProcesses(string userId)
        {
            return await _processRepository.GetProcessByUserId(userId);
        }

        public async Task<bool> CreateProcess(CreateProcessBE process)
        {

            Process processDto = _mapper.Map<Process>(process);
            return await _processRepository.CreateProcess(processDto);
        }
    }
}
