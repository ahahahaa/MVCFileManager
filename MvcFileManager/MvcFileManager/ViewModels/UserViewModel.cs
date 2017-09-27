using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcFileManager.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public bool isUpload { get; set; }

        public bool isSearch { get; set; }

        public bool isModify { get; set; }

    }
}