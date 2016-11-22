using System;
using System.Collections.Generic;

namespace Sensing.Entities.Things
{
    /// <summary>
    /// 事物需要人给它打标签,让它成为一个特定的东西.
    /// </summary>
    public class ThingTag : EntityBase
    {
        public string Name { get; set; }

        public ICollection<Thing_ThingTag> Thing_ThingTags { get; set; }
    }
}