using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Odev_Dagitim_Portali.Models
{
    public class Homework
    {
        [Key]
        public int Homework_id {  get; set; }



        public string Homework_title { get; set; }
        public string Homework_content { get; set; }


        public DateTime Homework_deadline { get; set; }

        [ForeignKey("Lessons")]
        public int Lesson_id { get; set; }

        [ForeignKey("AppUsers")]
        public string User_id { get; set; } 
        public AppUser AppUsers { get; set; }
        public Lesson Lessons { get; set; }
        public List<Homework_submission> Homework_submissions { get; set; }    
        
    }
}
