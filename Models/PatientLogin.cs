using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMSMVC.Models
{
    public class PatientLogin
    {
        [MaxLength(20)]
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Patient Name")]
        public string PatientName { get; set; }

        [MaxLength(6)]
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Patient Password")]
        public string Password { get; set; }

    }
}
