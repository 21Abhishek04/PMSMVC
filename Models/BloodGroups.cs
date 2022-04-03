using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMSMVC.Models
{
    public class BloodGroups
    {

        public BloodGroups()
        {
          
        }
        [Key]
        public byte BloodGroupId { get; set; }


        public string BloodGroup { get; set; }


    }
}
