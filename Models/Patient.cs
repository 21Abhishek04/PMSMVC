using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMSMVC.Models
{
    public class Patient
    {
        public Patient()
        {
           
        }

        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public string Password { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Gender { get; set; }
        public decimal? Weight { get; set; }
        public byte? BloodGroupId { get; set; }
        public long? PhoneNumber { get; set; }

        public virtual BloodGroups BloodGroup { get; set; }
   
    }
}
