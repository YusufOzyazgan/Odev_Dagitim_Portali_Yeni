using System.ComponentModel.DataAnnotations;

namespace Odev_Dagitim_Portali.Models
{
    public class Class
    {
        [Key]
        public int Class_id { get; set; }

        public string Class_name { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<UserClass> UserClasses { get; set; }
        
    }
}
