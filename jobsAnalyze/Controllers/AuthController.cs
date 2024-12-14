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

        public async void SaveToFileTest()
        {
            
        }

        [HttpPost]
        public TestResponse TestCache(TestBody body)
        {
            string cacheKey = HashString($"{body.hev}_{body.year}_{body.month}");
            TestResponse res = _cache.Get<TestResponse>(cacheKey);
            if (res == null)
            {
                //go to db and retrieve
                res = new TestResponse()
                {
                    Name = "Alina",
                    Age = 29
                };
                _cache.Set<TestResponse>(cacheKey, res, TimeSpan.FromHours(5));
            }
            return res;
        }
        [HttpGet]
        public FileResult TestCsv()
        {
            List<TestBody> list = new List<TestBody>() {
            new TestBody()
            {
                hev = "700",
                year = "2023",
                month = "11"
            },
            new TestBody()
            {
                hev = "701",
                year = "2023",
                month = "11"
            },
            new TestBody()
            {
                hev = "702",
                year = "2023",
                month = "11"
            } };
            
            MemoryStream stream = _filesUtils.CreateCSV(list);
            return File(stream.ToArray(), "text/csv", "test");
            /*
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = "TemplateTest.csv";
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv"); */
        }

        static string HashString(string text, string salt = "")
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }

            // Uses SHA256 to create the hash
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                // Convert the string to a byte array first, to be processed
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                // Convert back to a string, removing the '-' that BitConverter adds
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }
        }
    }

    public class TestBody
    {
        public string hev { get; set; }
        public string year { get; set; }
        public string month { get; set; }
    }


    public class TestResponse
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
