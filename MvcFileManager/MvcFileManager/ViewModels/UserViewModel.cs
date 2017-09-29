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

        public bool isUploadPM { get; set; }

        public bool isSearchPM { get; set; }

        public bool isModifyPM { get; set; }

        public bool isDeletePM { get; set; }

    }
}