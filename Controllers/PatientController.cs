using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PMSMVC.Models;
using Microsoft.AspNetCore.Http;



using System;

using System.Collections.Generic;

using System.Linq;

using System.Net;

using System.Net.Http;

using System.Net.Http.Headers;

using System.Text;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PMSMVC.Controllers
{
    public class PatientController : Controller
    {
        string Baseurl = "http://localhost:61003/";
        public IActionResult PatientDashBoard()
        {
            return View();
        }

     
       

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            List<BloodGroups> PInfo = new List<BloodGroups>();
            if (HttpContext.Session.GetString("Jwtoken") == null)
            {
                return RedirectToAction("PatientLogin", "Patient");
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
                HttpResponseMessage Res = await client.GetAsync("api/BloodGroups");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    PInfo = JsonConvert.DeserializeObject<List<BloodGroups>>(Response);
                }
                ViewBag.BloodGroupId = new SelectList(PInfo, "BloodGroupId", "BloodGroup");
                return View();
            }
        }

     

        [HttpPost]
        public async Task<IActionResult> Register(Patient patient)
        {
           
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
                string endpoint = Baseurl + "api/Patient/AddPatient"; 
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        TempData["Patient"] = JsonConvert.SerializeObject(patient);
                        ViewBag.Message = "Registration Successful";
                        return RedirectToAction("RegistrationSuccessful");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Could not register Patient");
                        return View();
                    }
                }
            }
        }

        public IActionResult RegistrationSuccessful()
        {
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> BookAppointment()
        {
            List<Departments> PInfo = new List<Departments>();
            if (HttpContext.Session.GetString("Jwtoken") == null)
            {
                return RedirectToAction("PatientLogin", "Patient");
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
                HttpResponseMessage Res = await client.GetAsync("api/Departments");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.StatusCode == HttpStatusCode.OK)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    PInfo = JsonConvert.DeserializeObject<List<Departments>>(Response);
                }
                ViewBag.DepartmentId = new SelectList(PInfo, "DepartmentId", "DepartmentName");
                return View();
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetHistory(string id)
        {
            List<Appointment> PInfo = new List<Appointment>();
            if (HttpContext.Session.GetString("Jwtoken") == null)
            {
                return RedirectToAction("PatientLogin", "Patient");
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
                HttpResponseMessage Res = await client.GetAsync("api/Patient/GetHistory/" + id);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    PInfo = JsonConvert.DeserializeObject<List<Appointment>>(Response);
                }
                return View();
            }
        }






        [HttpGet]

        public async Task<IActionResult> GetDoctors()
        {
            List<Doctor> PInfo = new List<Doctor>();
            if (HttpContext.Session.GetString("Jwtoken") == null)
            {
                return RedirectToAction("PatientLogin", "Patient");
            }
            using (var client = new HttpClient())
            {
         
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
               
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Jwtoken"));
                HttpResponseMessage Res = await client.GetAsync("api/Patient/GetDoctors");
           
                if (Res.StatusCode == HttpStatusCode.OK)
                {
                  
                    var Response = Res.Content.ReadAsStringAsync().Result;
                  
                    PInfo = JsonConvert.DeserializeObject<List<Doctor>>(Response);
                }
               
                return View(PInfo);
            }
        }


        [HttpPost]
        public async Task<IActionResult> BookAppointment(Appointment appointment)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(appointment), Encoding.UTF8, "application/json");
                string endpoint = Baseurl + "api/Appointments";
                using (var Res = await client.PostAsync(endpoint, content))
                {

                    if (Res.IsSuccessStatusCode)
                    {
                        TempData["Appointment"] = JsonConvert.SerializeObject(appointment);
                        return RedirectToAction("PatientDashBoard");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Could not Book Appointment.");
                        return View();
                    }
                }
            }

        }

        public IActionResult PatientLogout()
        {
            HttpContext.Session.Remove("Jwtoken");
            return RedirectToAction("PatientLogin", "Patient");

        }


        [HttpGet]
        public IActionResult PatientLogin()
        {
            
            return View();
        }



        [HttpPost]

        public async Task<IActionResult> PatientLogin(PatientLogin patient)

        {

            using (HttpClient client = new HttpClient())

            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");

                string endpoint = Baseurl + "api/Patient/Login";
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

                return Redirect("~/Patient/PatientDashBoard");

            }

        }

    }
}
