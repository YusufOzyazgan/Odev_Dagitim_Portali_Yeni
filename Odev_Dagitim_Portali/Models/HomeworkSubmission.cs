using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Odev_Dagitim_Portali.Models
{
    public class HomeworkSubmission
    {
        [Key]
        public int Submission_id { get; set; }
        public string File_name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        [ForeignKey("Homeworks")]
        public int Homework_id {  get; set; }
        public Homework Homeworks { get; set; }

        [ForeignKey("AppUsers")]
        public string User_id { get; set; }  

        public AppUser AppUsers { get; set; }
      
    }
}
