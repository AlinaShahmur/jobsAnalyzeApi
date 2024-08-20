using jobsAnalyze.Models.Auth.BE;
using jobsAnalyze.Models.Auth.Requests;
using jobsAnalyze.ResponseWrapper;

namespace jobsAnalyze.Business_Logic.Interfaces
{
    public interface IAuthService
    {
        public Task<ResponseBE> Register(RegisterUserBE userToRegister);
        public Task<ResponseBE> Login(LoginUserBE loginRequest);
        //public Task<ResponseBE> Logout(string userId);
    }
}
