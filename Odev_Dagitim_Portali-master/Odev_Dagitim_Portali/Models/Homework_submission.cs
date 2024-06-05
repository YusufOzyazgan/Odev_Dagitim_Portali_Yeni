using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Odev_Dagitim_Portali.Models
{
    public class Homework_submission
    {
        [Key]
        public int Submission_id { get; set; }
        public string File_name { get; set; }

        [ForeignKey("Homeworks")]
        public int Homework_id {  get; set; }


        [ForeignKey("AppUsers")]
        public string User_id { get; set; }  

        public AppUser AppUsers { get; set; }
        public Homework Homeworks { get; set; }
    }
}
