using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PMSMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PMSMVC.Controllers
{
    public class PatientsController : Controller
    {
        string Baseurl1 = "http://localhost:61003/";

        [HttpGet]

        public async Task<ActionResult> Index()
        {
            List<Appointment> PInfo = new List<Appointment>();
           
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl1);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Appointments/Get");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    PInfo = JsonConvert.DeserializeObject<List<Appointment>>(Response);
                }
                //returning the employee list to view
                return View(PInfo);
            }
        }




        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            Patient PInfo = new Patient();
            

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl1);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/HospitalEmp/PatientById/" + id);



                if (Res.IsSuccessStatusCode)
                {

                    var Response = Res.Content.ReadAsStringAsync().Result;

                    PInfo = JsonConvert.DeserializeObject<Patient>(Response);

                }
                return View(PInfo);
            }

        }



        [HttpPost]
        public async Task<IActionResult> Edit(string id, Doctor doctor)
        {


            using (HttpClient client = new HttpClient())
            {


                StringContent content = new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json");
                string endpoint = this.Baseurl1 + "api/HospitalEmp/PutDoctor/" + id;
                using (var Response = await client.PutAsync(endpoint, content))
                {

                    if (Response.IsSuccessStatusCode)
                    {
                        TempData["Doctor"] = JsonConvert.SerializeObject(doctor);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Could not update doctor");
                        return View();
                    }


                }
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Appointment appointment  = new Appointment();
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(appointment), Encoding.UTF8, "application/json");
                string endpoint = this.Baseurl1 + "api/Appointments/" + id;
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.IsSuccessStatusCode)
                    {
                        TempData["Appointment"] = JsonConvert.SerializeObject(appointment);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Could not delete doctor");
                        return View();
                    }
                }
            }
        }




        [HttpGet]

        public async Task<IActionResult> BookAppointment()
        {
            List<Departments> PInfo = new List<Departments>();
          
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl1);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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

        [HttpPost]
        public async Task<IActionResult> BookAppointment(Appointment appointment)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(appointment), Encoding.UTF8, "application/json");
                string endpoint = Baseurl1 + "api/Appointments";
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

       







       

    }
}
