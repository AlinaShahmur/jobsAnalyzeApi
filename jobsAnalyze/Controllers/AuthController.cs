using AutoMapper;
using jobsAnalyze.Business_Logic.Interfaces;
using jobsAnalyze.Helpers;
using jobsAnalyze.Helpers.Interfaces;
using jobsAnalyze.Models;
using jobsAnalyze.Models.Auth.BE;
using jobsAnalyze.Models.Auth.Requests;
using jobsAnalyze.ResponseWrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Caching.Memory;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Text;

namespace jobsAnalyze.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IAuthService _authService;
        private IMemoryCache _cache;
        private IFilesUtils _filesUtils;
        public AuthController(IMapper mapper, IAuthService authService, IMemoryCache cache, IFilesUtils fileUtils)
        {
            _mapper = mapper;
            _authService = authService;
            _cache = cache;
            _filesUtils = fileUtils;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Register(RegisterRequestModel request) {
            RegisterUserBE registerUserBE = _mapper.Map<RegisterUserBE>(request);
            BaseResponse res = _mapper.Map<BaseResponse>(await _authService.Register(registerUserBE));
            return StatusCode(res.Code, res);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Login([FromBody] LoginUserRequest loginRequest)
        {
            LoginUserBE loginUserBE = _mapper.Map<LoginUserRequest, LoginUserBE>(loginRequest);
            BaseResponse res = _mapper.Map<ResponseBE, BaseResponse>(await _authService.Login(loginUserBE));
            return StatusCode(res.Code, res);
        }
    }
}
