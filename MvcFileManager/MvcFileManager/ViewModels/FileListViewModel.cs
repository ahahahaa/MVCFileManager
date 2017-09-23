using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcFileManager.ViewModels
{
    public class FileListViewModel
    {
        public List<FileViewModel> FileList { get; set; }

        public string UserName { get; set; }
    }
}