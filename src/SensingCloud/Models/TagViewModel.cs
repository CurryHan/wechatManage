using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SensingCloud.Models
{

    public class TagViewModel
    {
        [Display(Name = "名称")]
        [Required]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long and less than {1}.", MinimumLength = 1)]
        public string TagName { get; set; }

        public int TagID { get; set; }
    }

}