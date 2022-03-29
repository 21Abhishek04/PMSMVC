using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMSMVC.Models
{
    public class Appointment
    {
        public string AppointmentId { get; set; }
        public string PatientId { get; set; }
        public byte? DepartmentId { get; set; }
        public string DoctorId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string Status { get; set; }
        public decimal? ConsultationFees { get; set; }

        public virtual Departments Department { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
