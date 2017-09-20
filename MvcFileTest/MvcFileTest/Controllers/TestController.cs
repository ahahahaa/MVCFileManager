using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFileTest.Models;

namespace MvcFileTest.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetView()
        {
            Employee e = new Employee();
            e.FirstName = "Zhechen";
            e.LastName = "Zhang";
            e.Salary = 200000;
            ViewBag.Employee = e;
            return View("GetView");
        }

    }
}
