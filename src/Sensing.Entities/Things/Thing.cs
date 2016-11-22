using System;
using System.Collections;
using System.Collections.Generic;

namespace Sensing.Entities.Things
{
    /// <summary>
    /// 世间万物,你能想到它有什么共同的属性,都可以在此列出来.
    /// </summary>
    public class Thing : EntityBase
    {
        /// <summary>
        /// 人可识别的唯一ID号码,如 Product-Xl-8890
        /// </summary>
        public string IdentifyingId { get; set; }

        /// <summary>
        /// 事物所属的所有者,也许它是别人的共有财产呢?
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// 事物都该有个名字来表示
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 在当今社会,任何事物都是可以明码标价的,难道不是!
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 二维码这么流行,没个这玩意都不好意思说我在编程.
        /// </summary>
        public string QRCodeUrl { get; set; }

        /// <summary>
        /// 万物总有属于他自己的关键字,让别人好找到它.
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// 有了分类是不是可以按照树形结构查找呢,可能,先留着吧。
        /// </summary>
        public string Categories { get; set; }

        public ICollection<Thing_ThingTag> Thing_ThingTags { get; set; }

        public ICollection<Thing_ThingResource> Thing_ThingResources { get; set; }
    }

    public class Thing_ThingTag
    {
        //public int Id { get; set; }
        public int ThingId { get; set; }
        public Thing Thing { get; set; }
        public int ThingTagId { get; set; }
        public ThingTag ThingTag { get; set; }
    }

    public class Thing_ThingResource
    {
        //public int Id { get; set; }
        public int ThingId { get; set; }
        public Thing Thing { get; set; }
        public int ThingResourceId { get; set; }
        public ThingResource ThingResource { get; set; }
    }
}