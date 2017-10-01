using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcFileManager.Filters
{
    //public bool IsCheck {get; set; }

    public class PermissionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        { 
            if(filterContext.HttpContext.Session["Permission"] == null)
            {
                //filterContext.Result = new ContentResult()
                //{
                //    Content="Insufficient permissions!"
                //};
                filterContext.HttpContext.Response.Redirect("/Account/Login");
            }
            
        }
    }
}