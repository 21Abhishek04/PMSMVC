using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMSMVC.Controllers
{
    public class BloodGroupsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
