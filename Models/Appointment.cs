using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMSMVC.Models
{
    public class Appointment
    {
     
        public int AppointmentId { get; set; }
        [MaxLength(6)]
        [Required]
        [DisplayName("Patient Id")]
        public string PatientId { get; set; }

        [Required]
        public byte? DepartmentId { get; set; }

        [MaxLength(6)]
        [Required]
        [DisplayName("Doctor Id")]
        public string DoctorId { get; set; }

        [Required]
        public DateTime? AppointmentDate { get; set; }
        public string Status { get; set; }

        public decimal? ConsultationFees { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Departments Department { get; set; }

        [ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
