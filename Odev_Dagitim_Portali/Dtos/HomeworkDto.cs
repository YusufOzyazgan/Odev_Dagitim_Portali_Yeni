using Odev_Dagitim_Portali.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Odev_Dagitim_Portali.Dtos
{
    public class HomeworkDto
    {
        public int Homework_id { get; set; }

        public string Homework_title { get; set; }
        public string Homework_content { get; set; }
        public int Lesson_id { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Homework_deadline { get; set; }

       
        
        public string? User_id { get; set; }

    }
}
