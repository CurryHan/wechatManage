using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using rs = Resources.Resources;

namespace SensingCloud.Models
{
    public class ActivityAddAdsViewModel
    {
        //[Required]
        [Display(Name = "标签")]
        [MaxLength(30)]
        public string Tags { get; set; }

        public string TagValueIDs { get; set; }
    }
}