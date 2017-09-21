using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFileTest.Models;
using MvcFileTest.ViewModels;

namespace MvcFileTest.Controllers
{
    public class EmployeeController : Controller
    {
        

        public ActionResult GetView()
        {
            Employee e = new Employee();
            e.FirstName = "Zhechen";
            e.LastName = "Zhang";
            e.Salary = 200000;

            //use ViewData
            //ViewData["Employee"] = e;
            //return View("GetView");

            //use ViewBag
            //ViewBag.Employee = e;
            //return View("GetView");

            //return View("GetView", e);

            //Model对象强制转换成ViewModel对象，在controller中处理逻辑关系
            EmployeeViewModel ve = new EmployeeViewModel();
            ve.EmployeeName = e.FirstName + " " + e.LastName;
            ve.Salary = e.Salary.ToString("C");
            if (e.Salary > 250000)
            {
                ve.SalaryColor = "yellow";
            }
            else
            {
                ve.SalaryColor = "green";
            }
            //ve.UserName = "Admin";
            return View("GetView", ve);

        }

        public ActionResult Index()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();

            List<Employee> employees = empBal.GetEmployees();

            List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();

            foreach (Employee emp in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
                empViewModel.Salary = emp.Salary.ToString("C");
                if (emp.Salary > 15000)
                {
                    empViewModel.SalaryColor = "yellow";
                }
                else
                {
                    empViewModel.SalaryColor = "green";
                }
                empViewModels.Add(empViewModel);
            }

            employeeListViewModel.Employees = empViewModels;
            //employeeListViewModel.UserName = "Admin";
            return View("Index", employeeListViewModel);

        }

        public ActionResult AddNew()
        {
            return View("CreateEmployee");
        }

        public ActionResult SaveEmployee(Employee e, string BtnSubmit)
        {
            //Model Binder: input "First Name"'s name matches e.FirstName
            //无参情况下，form语法和手动构建Model对象
            //Employee e = new Employee();
            //e.FirstName = Request.Form["FName"];
            //e.LastName = Request.Form["LName"];
            //e.Salary = int.Parse(Request.Form["Salary"])；


            //按钮功能逻辑，也是匹配input name
            switch (BtnSubmit)
            {
                case "Save Employee":
                    if (ModelState.IsValid)
                    {
                        //调用业务逻辑层SaveEmployee函数
                        EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
                        empBal.SaveEmployee(e);
                        return RedirectToAction("Index");

                        //return Content(e.FirstName + "|" + e.LastName + "|" + e.Salary);
                    }
                    else
                    {
                        return View("CreateEmployee");
                    }
                case "Cancel":
                    return RedirectToAction("Index");
            }
            //空白屏幕
            return new EmptyResult();
            
        }

    }
}
