using System.ComponentModel.DataAnnotations;

namespace Odev_Dagitim_Portali.Models
{
    public class University_department
    {
        [Key]
        public int Department_id { get; set; }  

        public string Department_name { get;}
        public List<Lesson> Lessons { get; set; }
        
        public AppUser AppUsers { get; set; }    
    }
}
