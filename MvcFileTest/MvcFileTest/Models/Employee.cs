using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcFileTest.Models
{
    public class Employee
    {
        //[FirstNameValidation]
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Enter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter Second Name")]
        public string LastName { get; set; }

        //[RegularExpression(@"[0-9]+", ErrorMessage = "Enter Number")]
        public int? Salary { get; set; }
        //public int Salary { get; set; }

    }
}