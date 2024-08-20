using AutoMapper;
using jobsAnalyze.Models.Auth.Requests;
using jobsAnalyze.Models.Auth.BE;
using jobsAnalyze.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace jobsAnalyze.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile() 
        {
            CreateMap<RegisterRequestModel, RegisterUserBE>()
                   .ForMember(r => r.Email, opt => opt.MapFrom(src => src.UserEmail));
            CreateMap<RegisterUserBE, IdentityUser>()
                    .ForMember(u => u.UserName, opt => opt.MapFrom(src => src.Username));
            CreateMap<LoginUserRequest, LoginUserBE>();
        }
    }
}
