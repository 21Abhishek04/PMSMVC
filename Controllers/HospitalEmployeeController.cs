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

    public class HospitalEmployeeController : Controller
    {
        string Baseurl = "http://localhost:61003/";

     
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<Doctor> PInfo = new List<Doctor>();
            if (HttpContext.Session.GetString("Jwtoken") == null)
            {
                return RedirectToAction("HospitalEmployeeLogin", "HospitalEmployee");
            }
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/HospitalEmp");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    PInfo = JsonConvert.DeserializeObject<List<Doctor>>(Response);
                }
                //returning the employee list to view
                return View(PInfo);
            }
        }

        public IActionResult HospitalEmployeeDashBoard()
        { 
            return View();
        }

        //drop down
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<Departments> PInfo = new List<Departments>();
            if (HttpContext.Session.GetString("Jwtoken") == null)
            {
                return RedirectToAction("HospitalEmployeeLogin", "HospitalEmployee");
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
                if (Res.IsSuccessStatusCode)
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


        //Appointment List
        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            List<Appointment> PInfo = new List<Appointment>();
            if (HttpContext.Session.GetString("Jwtoken") == null)
            {
                return RedirectToAction("HospitalEmployeeLogin", "HospitalEmployee");
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
                HttpResponseMessage Res = await client.GetAsync("api/HospitalEmp/GetAppointments");
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
        public async Task<IActionResult> Create(Doctor doctor)
        {

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json");
                string endpoint = Baseurl + "api/HospitalEmp";
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.IsSuccessStatusCode)
                    {
                        TempData["Doctor"] = JsonConvert.SerializeObject(doctor);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Could not add doctor");
                        return View();
                    }
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
           
            Doctor PInfo = new Doctor();
            if (HttpContext.Session.GetString("Jwtoken") == null)
            {
                return RedirectToAction("HospitalEmployeeLogin", "HospitalEmployee");
            }

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Jwtoken"));
                HttpResponseMessage Res = await client.GetAsync("api/HospitalEmp/GetDoctorById/" + id);
                
                //HttpResponseMessage Res1 = await client.GetAsync("api/HospitalEmploy/" + id);


                if (Res.IsSuccessStatusCode)
                {

                    var Response = Res.Content.ReadAsStringAsync().Result;
                   

                    PInfo = JsonConvert.DeserializeObject<Doctor>(Response);
                

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
                string endpoint = this.Baseurl + "api/HospitalEmp/PutDoctor/" + id;
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
        public async Task<IActionResult> Delete(string id)
        {
            Doctor doctor = new Doctor();
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json");
                string endpoint = this.Baseurl + "api/HospitalEmp/" + id;
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.IsSuccessStatusCode)
                    {
                        TempData["Doctor"] = JsonConvert.SerializeObject(doctor);

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


        //For Patients


        [HttpGet]
       
            public async Task<ActionResult> GetPatients()
            {
                List<Patient> PInfo = new List<Patient>();
            if (HttpContext.Session.GetString("Jwtoken") == null)
            {
                return RedirectToAction("HospitalEmployeeLogin", "HospitalEmployee");
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
                HttpResponseMessage Res = await client.GetAsync("api/HospitalEmp/GetPatient");
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


     

        public async Task<IActionResult> AddPatient()
        {
            List<BloodGroups> PInfo = new List<BloodGroups>();
            if (HttpContext.Session.GetString("Jwtoken") == null)
            {
                return RedirectToAction("HospitalEmployeeLogin", "HospitalEmployee");
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
                    ViewBag.BloodGroupId = new SelectList(PInfo, "BloodGroupId", "BloodGroup");

                }

                return View();

            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient(Patient patient)
        {

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
                string endpoint = Baseurl + "api/HospitalEmp/AddPatient/";
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.IsSuccessStatusCode)
                    {
                        TempData["Patient"] = JsonConvert.SerializeObject(patient);

                        return RedirectToAction("GetPatients");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Could not add doctor");
                        return View();
                    }
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditPatient(string id)
        {
            Patient PInfo = new Patient();
            if (HttpContext.Session.GetString("Jwtoken") == null)
            {
                return RedirectToAction("HospitalEmployeeLogin", "HospitalEmployee");
            }

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Jwtoken"));
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
        public async Task<IActionResult> EditPatient(string id, Patient patient)
        {


            using (HttpClient client = new HttpClient())
            {


                StringContent content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
                string endpoint = this.Baseurl + "api/HospitalEmp/PutPatient/" + id;
                using (var Response = await client.PutAsync(endpoint, content))
                {

                    if (Response.IsSuccessStatusCode)
                    {
                        TempData["Patient"] = JsonConvert.SerializeObject(patient);
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Updated Successfully");
                        return RedirectToAction("GetPatients");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Could not update Patient");
                        return View();
                    }


                }
            }
        }


        [HttpGet]

        public async Task<ActionResult> SearchPatients(string  search)
        {
            List<Patient> PInfo = new List<Patient>();
            if (HttpContext.Session.GetString("Jwtoken") == null)
            {
                return RedirectToAction("HospitalEmployeeLogin", "HospitalEmployee");
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Jwtoken"));
                HttpResponseMessage Res = await client.GetAsync("api/HospitalEmp/SearchPatient/"+search);
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
            
                    PInfo = JsonConvert.DeserializeObject<List<Patient>>(Response);
                }
                return View(PInfo);
            }
        }
        public IActionResult HospitalEmployeeLogout()
        {
            HttpContext.Session.Remove("Jwtoken");
            return RedirectToAction("HospitalEmployeeLogin", "HospitalEmployee");

        }


        [HttpGet] // and also create a post method

        public IActionResult HospitalEmployeeLogin()

        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> HospitalEmployeeLogin(AdminLogin adminLogin)

        {
            using (HttpClient client = new HttpClient())

            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(adminLogin), Encoding.UTF8, "application/json");

                string endpoint = Baseurl + "api/HospitalEmp/Login";

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
               
                  return Redirect("~/HospitalEmployee/HospitalEmployeeDashBoard");
            }

        }
    }
}



