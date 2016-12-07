using Sensing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SensingCloud.Models
{
    public class TicketViewModel
    {
        public string Name { get; set; }
        public int? MediaId { get; set; }
        public Media Media { get; set; }

        public string Thumb_url { get; set; }
    }
}