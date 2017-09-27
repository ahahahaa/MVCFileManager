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
                    uvm.isUpload = user.isUpload;
                    uvm.isSearch = user.isSearch;
                    uvm.isModify = user.isModify;
                    uvmlist.Add(uvm);
                }
            }

            ulvm.UserList = uvmlist;
            ulvm.isAdmin = true;

            return View("UserIndex", ulvm);
        }

    }
}
