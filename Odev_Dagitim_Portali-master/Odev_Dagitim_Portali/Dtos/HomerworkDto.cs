using Odev_Dagitim_Portali.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Odev_Dagitim_Portali.Dtos
{
    public class HomerworkDto
    {
        public int Submission_id { get; set; }

        public string File_name { get; set; }
 
        public int Homework_id { get; set; }

        public string User_id { get; set; }

       
    }
}
