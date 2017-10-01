using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcFileManager.Models;

namespace MvcFileManager.ViewModels
{
    public class VersionViewModel
    {
        public FileDB FirstVersion {get; set; }

        public FileDB SecondVersion { get; set; }

        public string DiffContent { get; set; }
    }
}