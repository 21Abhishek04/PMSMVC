using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PMSMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PMSMVC.Controllers
{
    public class DoctorController : Controller
    {
        string Baseurl = "http://localhost:61003/";



        public async Task<ActionResult> GetAllPatients()
        {
            List<Patient> PInfo = new List<Patient>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Doctor");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    PInfo = JsonConvert.DeserializeObject<List<Patient>>(Response);
                }
                //returning the employee list to view
                return View(PInfo);
            }
        }

    }
}
