using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoApp.WebMvc.Controllers
{
    public class EmployeesKnockOutController : Controller
    {
        // GET: EmployeesKnockOut
        public ActionResult Index()
        {
            return View();
        }
    }
}