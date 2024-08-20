namespace jobsAnalyze.Models
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public string? Message { get; set; }
        public object Data {  get; set; }
    }
    //public class ErrorResponse : BaseResponse
    //{
    //    public 
    //}
}
