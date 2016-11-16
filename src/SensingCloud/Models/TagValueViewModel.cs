using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SensingCloud.Models
{
    public class TagValueViewModel
    {
        [Display(Name = "内容")]
        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long and less than {1}.", MinimumLength = 1)]
        public string TagValue { get; set; }
        public string TagPinYinValue { get; set; }
        public int TagID { get; set; }

        public int TagValueID { get; set; }

        [Required]
        [Display(Name = "Group名")]
        public int GroupID { get; set; }
    }
}