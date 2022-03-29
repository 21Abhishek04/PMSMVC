using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMSMVC.Models
{
    public  class Doctor
    {
        public Doctor()
        {
          
        }

        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Password { get; set; }
        public byte? DepartmentId { get; set; }
        public string Specializations { get; set; }
        public string Qualification { get; set; }
        public decimal? ConsultationFees { get; set; }

        public virtual Departments Department { get; set; }

        internal static object Find(string id)
        {
            throw new NotImplementedException();
        }

        internal static void Remove(Doctor emp)
        {
            throw new NotImplementedException();
        }
    }
}
