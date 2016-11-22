using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SensingCloud.Models
{
    public class HeatMapViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<List<int>> Data { get; set; }
        public string Background { get; set; }
        public string Ip { get; set; }
    }
}