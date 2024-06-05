using System.ComponentModel.DataAnnotations;

namespace Odev_Dagitim_Portali.Dtos
{
    public class UserClassDto
    {
        public int? Id { get; set; }
        public string UserId { get; set; }
        public int ClassId { get; set; }
        public string? UserName { get; set; }   
        public string? ClassName { get; set; }
    }
}
