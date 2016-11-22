using Sensing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sensing.Entities.Versions
{
    /// <summary>
    /// 程序更新版本的Model类,主要用户管理Client的版本更新.
    /// </summary>
    public class SoftwareClientDetails : EntityBase
    {
        /// <summary>
        /// 版本号,格式为 1.0.0.5
        /// </summary>
        public string VersionNumber { get; set; }

        /// <summary>
        /// 特定版本的开发者代号,如Spartan.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 软件更新的Patch地址,用户客户端下载. /software/v1.0.0.zip
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 软件更新Patch的全途径, 如https://dns.troncell.com/software/v1.0.0.zip
        /// </summary>
       [NotMapped]
        public string FullLink { get; set; }
    }
}