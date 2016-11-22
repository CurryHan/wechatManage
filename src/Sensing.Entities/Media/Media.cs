using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.Entities
{
    public class Media:EntityBase
    {
        public string MediaKey { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }
    }
}
