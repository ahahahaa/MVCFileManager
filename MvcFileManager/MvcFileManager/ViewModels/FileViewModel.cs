using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcFileManager.ViewModels
{
    public class FileViewModel
    {
        public string FileName { get; set; }

        public string Author { get; set; }

        public DateTime UploadTime { get; set; }

        public int Version { get; set; }

    }
}