using AutoMapper;
using jobsAnalyze.Business_Logic.Interfaces;
using jobsAnalyze.Models;
using jobsAnalyze.Models.Auth.BE;
using jobsAnalyze.Models.Auth.Requests;
using jobsAnalyze.ResponseWrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jobsAnalyze.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IAuthService _authService;
        public AuthController(IMapper mapper, IAuthService authService) 
        {
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Register(RegisterRequestModel request) {
            RegisterUserBE registerUserBE = _mapper.Map<RegisterUserBE>(request);
            BaseResponse res = _mapper.Map<ResponseBE, BaseResponse>(await _authService.Register(registerUserBE));
            return StatusCode(res.Code, res);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Login(LoginUserRequest loginRequest)
        {
            LoginUserBE loginUserBE = _mapper.Map<LoginUserRequest, LoginUserBE>(loginRequest);
            BaseResponse res = _mapper.Map<ResponseBE, BaseResponse>(await _authService.Login(loginUserBE));
            return StatusCode(res.Code, res);
        }
    }
}
