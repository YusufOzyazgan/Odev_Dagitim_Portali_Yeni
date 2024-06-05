using System.ComponentModel.DataAnnotations.Schema;

namespace Odev_Dagitim_Portali.Dtos
{
    public class HomeworkSubmissionDto
    {
        public int? Submission_id { get; set; }
        public IFormFile? file { get; set; }
        public string File_name { get; set; }
        public int Homework_id { get; set; }
        public string? User_id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

    }
}
