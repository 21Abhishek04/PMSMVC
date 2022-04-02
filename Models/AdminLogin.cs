using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMSMVC.Models
{
    public class AdminLogin
    {
        [MaxLength(20)]
        [Required]
        [DisplayName("Admin Name")]
        public string HemployeeName { get; set; }

        [MaxLength(6)]
        [Required]
        [DisplayName("Admin Password")]
        public string Password { get; set; }
    }
}
