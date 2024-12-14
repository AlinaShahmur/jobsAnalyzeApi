using AutoMapper;
using jobsAnalyze.Models.Application.BE;
using jobsAnalyze.Models.Application.Requests;
using jobsAnalyze.Models.Auth.BE;
using jobsAnalyze.Models.Auth.Requests;
using jobsAnalyze.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace jobsAnalyze.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<CreateApplicationRequest, CreateApplicationBE>();
            CreateMap<CreateApplicationBE, Application>();
        }
    }
}
