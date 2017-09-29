using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFileManager.Models;
using MvcFileManager.ViewModels;
using MvcFileManager.Business_Layer;
using MvcFileManager.Data_Access_Layer;

namespace MvcFileManager.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult UserIndex()
        {
            UserListViewModel ulvm = new UserListViewModel();
            UserBusinessLayer ubl = new UserBusinessLayer();
            List<UserProfile> users = ubl.GetUsers();
            List<UserViewModel> uvmlist = new List<UserViewModel>();

            foreach (UserProfile user in users)
            {
                if (!user.isDeleteUser)
                {
                    UserViewModel uvm = new UserViewModel();
                    uvm.UserId = user.UserId;
                    uvm.UserName = user.UserName;
                    uvm.isUploadPM = user.isUploadPM;
                    uvm.isSearchPM = user.isSearchPM;
                    uvm.isModifyPM = user.isModifyPM;
                    uvm.isDeletePM = user.isDeletePM;
                    uvmlist.Add(uvm);
                }
            }

            ulvm.UserList = uvmlist;
            ulvm.isAdmin = true;

            return View("UserIndex", ulvm);
        }

        //
        //Post: /User/SavePermission
        public ActionResult SavePermission()
        {
            int UserId = int.Parse(Request["UserId"]);
            System.Diagnostics.Debug.Write(UserId);
            var result = new { err = false, message = "no err" };

            UserBusinessLayer ubl = new UserBusinessLayer();
            UserProfile user = ubl.GetUser(UserId);

            if (user == null || user.isDeleteUser)
            {
                return HttpNotFound();
            }

            user.isSearchPM = bool.Parse(Request["isSearchPM"]);
            //if (user.isSearchPM) { return Content("true"); }
            //else { return Content("false" + Request["isSearchPM"]); }
            user.isUploadPM = bool.Parse(Request["isUploadPM"]);
            user.isModifyPM = bool.Parse(Request["isModifyPM"]);
            user.isDeletePM = bool.Parse(Request["isDeletePM"]);

            user = ubl.ModifyUser(user);
            if (user.isDeleteUser)
            {
                result = new { err = true, message = "Error occurs, modify failed" };
                return Json(result);
            }
            return Json(result);
        }


        //
        //Post: /User/Delete
        public ActionResult Delete(FormCollection fcNotUsed, int id)
        {
            UserBusinessLayer ubl = new UserBusinessLayer();
            UserProfile user = ubl.GetUser(id);

            if (user == null || user.isDeleteUser)
            {
                return HttpNotFound();
            }

            user.isDeleteUser = true;
            ubl.ModifyUser(user);

            if (!user.isDeleteUser)
            {
                return Content("Error occurs");
            }
            return RedirectToAction("UserIndex");
        }

    }
}
