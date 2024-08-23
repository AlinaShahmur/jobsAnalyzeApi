using AutoMapper;
using jobsAnalyze.Models.DTO;
using jobsAnalyze.Models.Process;
using jobsAnalyze.Models.Process.BE;

namespace jobsAnalyze.Profiles
{
    public class ProcessProfile : Profile
    {
        public ProcessProfile() 
        {
            CreateMap<CreateProcessRequest, CreateProcessBE>()
                .ForMember(p => p.UserId, opt => opt.Ignore());
            CreateMap<CreateProcessBE, Process>();
        }
    }
}
