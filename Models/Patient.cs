using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMSMVC.Models
{
    public class Patient
    {
        public Patient()
        {
           
        }
        [Key]
        [Required]
        [DisplayName("Patient Id")]
        public string PatientId { get; set; }

        [Required]
        [DisplayName("Patient Name")]
        public string PatientName { get; set; }

        [Required]
        [MaxLength(6)]
        public string Password { get; set; }

        [Required]
        [DisplayName("Birth Date")]
        public DateTime? Birthdate { get; set; }

        [Required]
        [MaxLength(1)]
        [DisplayName("Gender ( M/F)")]
        public string Gender { get; set; }
        [Required]
        public decimal? Weight { get; set; }
        [Required]
        [DisplayName("Blood Group Id")]
        public byte? BloodGroupId { get; set; }
        [Required]
        [DisplayName("Phone Number")]
        public long? PhoneNumber { get; set; }

        [ForeignKey("BloodGroupId")]
        public virtual BloodGroups BloodGroup { get; set; }
   
    }
}
