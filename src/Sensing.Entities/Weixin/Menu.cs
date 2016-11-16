using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.Entities
{
    public class Menu : EntityBase
    {
        public string Name { get; set; }

        public int MediaId { get; set; }
        public virtual Media Media { get; set; }
    }
}
