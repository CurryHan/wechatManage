using System;
using System.Collections.Generic;

namespace Sensing.Entities.Things
{
    public enum ResourceType
    {
        None,
        Text,
        Image,
        Video,
        PPT,
        PDF,
        Web
    }
    /// <summary>
    /// 既然存在电脑里面了,肯定是电子档的资源,可以描述资源的。
    /// </summary>
    public class ThingResource : EntityBase
    {
        public string Name { get; set; }
        public string Content { get; set;}
        public string FileUrl { get; set; }
        
        public ResourceType Type { get; set; }

        public ICollection<Thing_ThingResource> Thing_ThingResources { get; set; }
    }
}