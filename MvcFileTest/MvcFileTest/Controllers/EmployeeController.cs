using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFileTest.Filter;
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
            ve.Salary = (e.Salary == null) ? "" : (Convert.ToInt32(e.Salary)).ToString("C");
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

        [Authorize]
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
                empViewModel.Salary = (emp.Salary == null) ? "" : (Convert.ToInt32(emp.Salary)).ToString("C");
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
            //View中显示UserName
            employeeListViewModel.UserName = User.Identity.Name;
            //employeeListViewModel.UserName = "Admin";
            return View("Index", employeeListViewModel);

        }
        
        [AdminFilter]
        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            { 
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }

        //可被/Employee/AddNew的URL直接访问，不安全！引入MVC Action过滤器
        [AdminFilter]       //绑定过滤器
        public ActionResult AddNew()
        {
            //再次请求Add New时，将上次输入验证错误时记录在CreateEmployeeViewModel中的值传入
            return View("CreateEmployee", new CreateEmployeeViewModel());
            //return View("CreateEmployee”);
        }


        [AdminFilter]
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
                        //错误验证值的保留
                        CreateEmployeeViewModel vm = new CreateEmployeeViewModel();
                        vm.FirstName = e.FirstName;
                        vm.LastName = e.LastName;
                        if (e.Salary.HasValue)
                        {
                            vm.Salary = e.Salary.ToString();                        
                        }
                        else
                        {
                            vm.Salary = ModelState["Salary"].Value.AttemptedValue;                       
                        }
                        return View("CreateEmployee", vm);
                        //return View("CreateEmployee");
                    }
                case "Cancel":
                    return RedirectToAction("Index");
            }
            //空白屏幕，在这里不会执行
            return new EmptyResult();
            
        }


    }
}
