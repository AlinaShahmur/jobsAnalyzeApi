using Microsoft.IdentityModel.Tokens;

namespace jobsAnalyze.ResponseWrapper
{
    public class ResponseBE
    {
        public int Code { get; set; }   
        public string Message  { get; set; }
        public object Data  { get; set; }
        
        public ResponseBE(int code, string message = "", object data = null) 
        { 
            Code = code;
            Message = message.IsNullOrEmpty() ? GetCommonMessageByCode(code) : message;
            Data = data;
        }
        public string GetCommonMessageByCode(int code)
        {
            return code >= 400 ? "Error" : "Success";
        }
    }
}
