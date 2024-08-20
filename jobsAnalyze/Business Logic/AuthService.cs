using AutoMapper;
using jobsAnalyze.Business_Logic.Interfaces;
using jobsAnalyze.Models.Auth.BE;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Net.Http;
using System.Web;
using Microsoft.EntityFrameworkCore;
using jobsAnalyze.Models.DTO;
using jobsAnalyze.ResponseWrapper;
using System.Resources;
using jobsAnalyze.Helpers;
using jobsAnalyze.Helpers.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Server.HttpSys;

namespace jobsAnalyze.Business_Logic
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthHelper _authHelper;
        private readonly IMapper _mapper;
        public AuthService(UserManager<IdentityUser> userManager, IAuthHelper authHelper, IMapper mapper)
        {
            _userManager = userManager;
            _authHelper = authHelper;
            _mapper = mapper;
        }
        public async Task<ResponseBE> Register(RegisterUserBE userToRegister)
        {   
            var user = await _userManager.FindByEmailAsync(userToRegister.Email);
            if (user == null)
            {
                await RegisterNewUser(userToRegister);
                return new ResponseBE(200);
            }
            string? msgUserExists = Resource.ResourceManager.GetString("MsgUserExists");
            return new ResponseBE(400, msgUserExists.IsNullOrEmpty() ? "" : msgUserExists);
        }

        public async Task<ResponseBE> Login(LoginUserBE loginUser)
        {
            IdentityUser? foundUser = await _userManager.FindByEmailAsync(loginUser.Email);
            if (foundUser != null)
            {
                bool res = await _userManager.CheckPasswordAsync(foundUser, loginUser.Password);
                if (res)
                { 
                    string token = this.SuccessfulLoginAction(foundUser.Id);
                    return new ResponseBE(200, "", token);
                }
            }
            return new ResponseBE(401);
        }

        //public async Task<ResponseBE> Logout(string userId)
        //{

        //}


        private async Task RegisterNewUser(RegisterUserBE user)
        {
            IdentityUser newUser = _mapper.Map<RegisterUserBE, IdentityUser>(user);
            IdentityResult res = await _userManager.CreateAsync(newUser, user.Password);
        }

        private string SuccessfulLoginAction(string userId)
        {
           return _authHelper.CreateToken(userId);
        }
    }
}
