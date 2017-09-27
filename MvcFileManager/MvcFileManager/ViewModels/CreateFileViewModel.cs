using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcFileManager.ViewModels
{
    public class CreateFileViewModel
    {
        
        public string FileName { get; set; }

        public int Version { get; set; }

        public string FilePath { get; set; }

        public int FileId { get; set; }

    }
}