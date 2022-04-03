using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMSMVC.Models
{
    public class Departments
    {

        public Departments()
        {

        }
            [Key]
            public byte DepartmentId { get; set; }
            public string DepartmentName { get; set; }


    }
    
}
