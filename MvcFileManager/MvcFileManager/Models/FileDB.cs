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
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Upload Time")]
        [DataType(DataType.DateTime, ErrorMessage = "Wrong Format")]
        public DateTime UploadTime { get; set; }

        [Required]
        [Display(Name = "Last Modified Time")]
        [DataType(DataType.DateTime, ErrorMessage = "Wrong Format")]
        public DateTime ModifiedTime { get; set; }

        [Required]
        [Display(Name = "Version")]
        public int Version { get; set; }

        [Required]
        public string FilePath { get; set; }

        [ForeignKey("FileId")]
        public virtual FileDB ParentId { get; set; }

        public bool isDelete { get; set; }

    }
}