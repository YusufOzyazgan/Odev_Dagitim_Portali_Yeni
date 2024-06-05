using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Odev_Dagitim_Portali.Models
{
    public class Lesson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Lesson_id { get; set; }

        public string Lesson_name { get; set; } 

        [ForeignKey("University_departments")]
        public int Department_id { get; set; }
        public University_department University_departments { get; set; }
        public List<Homework> Homeworks { get; set; }    
    }
}
