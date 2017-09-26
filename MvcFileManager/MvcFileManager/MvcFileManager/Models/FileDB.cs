using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcFileManager.Models
{
    public class FileDB
    {
        [Key]
        public int FileId { get; set; }

        [Required]
        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Required]
        [Display(Name = "Creater")]
        public string Creater { get; set; }

        [Required]
        [Display(Name = "Upload Time")]
        [DataType(DataType.DateTime, ErrorMessage = "Wrong Data Time Format")]
        public DateTime UploadTime { get; set; }

        [Required]
        [Display(Name = "Last Modified Time")]
        [DataType(DataType.DateTime, ErrorMessage = "Wrong Date Time Format")]
        public DateTime ModifiedTime { get; set; }

        [Required]
        [Display(Name = "Version")]
        public int Version { get; set; }

        public string FilePath { get; set; }

        public FileDB FormerId { get; set; }

        public bool isDelete { get; set; }

    }
}