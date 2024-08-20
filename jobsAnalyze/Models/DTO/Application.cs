using System.Data;

namespace jobsAnalyze.Models.DTO
{
    public class Application
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public DateTime StartDate { get; set; }
        public string Img { get; set; }
        public string CompanyName { get; set; }
        public int SourceId { get; set; }
        public int StatusId { get; set; }
        public int ProcessId { get; set; }
        public int CurrentStage { get; set; }
    }
}
