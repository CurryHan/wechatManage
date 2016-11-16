using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sensing.Entities
{
    public enum ContactType
    {
        Email,
        Phone
    }
    public class Contact : EntityBase
    {
        public ContactType Type { get; set; }
        public string Info { get; set; }
    }
}
