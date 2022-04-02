using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PMSMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PMSMVC.Controllers
{
    public class DoctorController : Controller
    {
        string Baseurl = "http://localhost:61003/";


        public IActionResult DoctorDashBoard()
        {
            return View();
        }
    
        [HttpGet] // and also create a post method

        public IActionResult DoctorLogin()

        {

            return View();

        }
        public IActionResult DoctorLogout()
        {
            HttpContext.Session.Remove("Jwtoken");
            return RedirectToAction("DoctorLogin", "Doctor");

        }

        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            List<Patient> PInfo = new List<Patient>();

                if (HttpContext.Session.GetString("Jwtoken") == null)
            {
                return RedirectToAction("DoctorLogin", "Doctor");
            }

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("Bearer",HttpContext.Session.GetString("Jwtoken"));


                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Doctor/GetPatients");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    PInfo = JsonConvert.DeserializeObject<List<Patient>>(Response);
                }
                return View(PInfo);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointment()
        {
            List<Appointment> PInfo = new List<Appointment>();
            if (HttpContext.Session.GetString("Jwtoken") == null)
            {
                return RedirectToAction("DoctorLogin", "Doctor");
            }

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Jwtoken"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Doctor/GetAppointment");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    PInfo = JsonConvert.DeserializeObject<List<Appointment>>(Response);
                }
                return View(PInfo);
            }
        }

        [HttpPost]

        public async Task<IActionResult> DoctorLogin(DoctorLogin doctorLogin)

        {
            using (HttpClient client = new HttpClient())

            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(doctorLogin), Encoding.UTF8, "application/json");

                string endpoint = Baseurl + "api/Doctor/Login";

                using (var Response = await client.PostAsync(endpoint, content))

                {
                    string token = await Response.Content.ReadAsStringAsync();

                    if (token == "User not found")

                    {
                        ViewBag.Message = "Invalid Credentials";

                        return View();
                    }

                    HttpContext.Session.SetString("Jwtoken", token);

                }

                return Redirect("~/Doctor/DoctorDashBoard");

            }

        }

    }
}
