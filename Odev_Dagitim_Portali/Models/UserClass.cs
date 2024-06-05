using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Odev_Dagitim_Portali.Models
{
    public class UserClass
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AppUsers")]
        public string UserId { get; set; }

        [ForeignKey("Classes")]
        public int ClassId { get; set; }    

        public Class Classes { get; set; }
        public AppUser AppUsers { get; set; } 

    }
}
