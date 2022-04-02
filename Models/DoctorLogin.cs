using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMSMVC.Models
{
    public class DoctorLogin
    {
        [MaxLength(15)]
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Doctor Name")]
       
        public string DoctorName { get; set; }

        [MaxLength(6)]
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Doctor Password")]
      
        public string Password { get; set; }
    }
}
