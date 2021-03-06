﻿using DemoApp.Global;
using DemoApp.WebMvc.Models;
using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DemoApp.WebMvc.Controllers
{
    //[EnableCors("AllowSpecificOrigin")]
    public class EmployeesSlickGridsController : Controller
    {
        // GET: EmployeesSlickGrids
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEmployees()
        {


            IEnumerable<ModelEmployeeMvc> EmpList;
            HttpResponseMessage httpResponseMessage = GlobalHttpClient.httpClient.GetAsync("Employees").Result;
            EmpList = httpResponseMessage.Content.ReadAsAsync<IEnumerable<ModelEmployeeMvc>>().Result;
            return Json(new { data = EmpList }, JsonRequestBehavior.AllowGet);

            //IEnumerable<ModelEmployeeMvc> EmpList;
            //HttpResponseMessage httpResponseMessage = GlobalHttpClient.httpClient.GetAsync("Employees").Result;
            //EmpList = httpResponseMessage.Content.ReadAsAsync<IEnumerable<ModelEmployeeMvc>>().Result;

            //var slickGridData = EmpList;
            //var jsonResult = Json(slickGridData, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;

        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new ModelEmployeeMvc());
            }
            else
            {
                HttpResponseMessage response = GlobalHttpClient.httpClient.GetAsync("Employees/" + id.ToString()).Result;
            
                return View(response.Content.ReadAsAsync<ModelEmployeeMvc>().Result);
            }

        }

        [HttpPost]
        public ActionResult AddOrEdit(ModelEmployeeMvc emp)
        {
            if (emp.Id == 0)
            {
                HttpResponseMessage response = GlobalHttpClient.httpClient.PostAsJsonAsync("Employees", emp).Result;
                //return RedirectToAction("Index", "EmployeesSlickGrids");
                return Json(new { success = true, message = "Saved successfully", JsonRequestBehavior.AllowGet });
            }
            else
            {
                HttpResponseMessage response = GlobalHttpClient.httpClient.PutAsJsonAsync("Employees/" + emp.Id, emp).Result;
                //return RedirectToAction("Index", "EmployeesSlickGrids");
                return Json(new { success = true, message = "Updated successfully", JsonRequestBehavior.AllowGet });

            }

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalHttpClient.httpClient.DeleteAsync("Employees/" + id).Result;
            return Json(new { success = true, message = "Deleted successfully", JsonRequestBehavior.AllowGet });
        }
    }
}