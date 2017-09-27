using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcFileManager.ViewModels
{
    public class UserListViewModel
    {
        public List<UserViewModel> UserList { get; set; }

        public bool isAdmin { get; set; }
    }
}