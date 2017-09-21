using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcFileTest.Data_Access_Layer;

namespace MvcFileTest.Models
{
    public class EmployeeBusinessLayer
    {
        //验证用户功能，硬编码
        //public bool IsValidUser(UserDetails u)
        //{
        //    if (u.UserName == "Admin" && u.PassWord == "Admin")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //用户权限管理
        public UserStatus GetUserValidity(UserDetails u)
        {
            if (u.UserName == "Admin" && u.PassWord == "Admin")
            {
                return UserStatus.AuthenticatedAdmin;
            }
            else if (u.UserName == "Zhechen" && u.PassWord == "Zhang")
            {
                return UserStatus.AuthenticatedUser;
            }
            else 
            {
                return UserStatus.NonAuthenticatedUser;
            }
        }


        public List<Employee> GetEmployees()
        {
            //硬编程
            //List<Employee> employees = new List<Employee>();

            //Employee emp = new Employee();
            //emp.FirstName = "johnson";
            //emp.LastName = " fernandes";
            //emp.Salary = 14000;
            //employees.Add(emp);

            //emp = new Employee();
            //emp.FirstName = "michael";
            //emp.LastName = "jackson";
            //emp.Salary = 16000;
            //employees.Add(emp);

            //return employees;

            SalesERPDAL salesDal = new SalesERPDAL();
            return salesDal.Employees.ToList();

        }

        //保存数据库记录，更新表格
        public Employee SaveEmployee(Employee e)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.Add(e);
            salesDal.SaveChanges();
            return e;
        }
    }
}