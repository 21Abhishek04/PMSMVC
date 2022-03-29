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

      
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<Doctor> PInfo = new List<Doctor>();
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

        

        [HttpPost]
        public async Task<IActionResult> Create(Doctor doctor)
        {

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json");
                string endpoint = this.Baseurl + "api/HospitalEmp/";
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


            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
          
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
              
                HttpResponseMessage Res = await client.GetAsync("api/HospitalEmp/" + id);
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
                string endpoint = this.Baseurl + "api/HospitalEmp/" + id;
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
                using (var Response = await client.PostAsync(endpoint,content))
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

        [HttpPost]
        public async Task<IActionResult> CreatePatient(Patient patient)
        {

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
                string endpoint = this.Baseurl + "api/HospitalEmp/";
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.IsSuccessStatusCode)
                    {
                        TempData["Patient"] = JsonConvert.SerializeObject(patient);

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

    }

    }




