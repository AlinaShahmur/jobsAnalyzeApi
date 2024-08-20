using AutoMapper;
using jobsAnalyze.Models;
using jobsAnalyze.ResponseWrapper;

namespace jobsAnalyze.Profiles
{
    public class BaseProfile : Profile
    {
        public BaseProfile() 
        {
            CreateMap<ResponseBE, BaseResponse>()
                .ForMember(
                    r => r.Success,
                    opt => opt.MapFrom(x => x.Code < 400)
                 );
        }

    }
}
