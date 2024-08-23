using System.ComponentModel.DataAnnotations;

namespace jobsAnalyze.Models.Process
{
    public class CreateProcessRequest
    {
        [Required]
        public string Name { get; set; }

    }
}
