using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMSMVC.Models
{
    public  class Doctor
    {
        public Doctor()
        {
          
        }
        [Key]
        [Required]
        [DisplayName("Doctor Id")]
        public string DoctorId { get; set; }

        [Required]
        [DisplayName("Doctor Name")]
        public string DoctorName { get; set; }
        [Required]
        [MaxLength(6)]
        [DisplayName("Password")]
        public string Password { get; set; }
        [Required]
        [MaxLength(2)]
        [DisplayName("Department Id")]
        public byte? DepartmentId { get; set; }
        [Required]
        [DisplayName("Specializations")]
        public string Specializations { get; set; }
        [Required]
        [DisplayName("Qualification")]
        public string Qualification { get; set; }
        [Required]
        [DisplayName("ConsultationFees")]
        public decimal? ConsultationFees { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Departments Department { get; set; }

       
    }
}
