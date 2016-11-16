using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sensing.Entities
{
    /// <summary>
    /// 目前不考虑一个设备进入多个组,有可能这是一个缺陷。
    /// </summary>
    public enum AuditStatus
    {
        //当前什么状态都不是,如新建一个设备后就是None,为默认值.
        None,
        //当前设备没有入组,处于
        [Display(Name = "下线")]
        Offline,
        //上线审核中...
        [Display(Name = "上线审核中")]
        OnlineAuditing,
        //审核通过,在线
        [Display(Name = "上线")]
        Online,
        //上线审核被拒绝.
        [Display(Name = "上线审核被拒绝")]
        OnlineAuditRejected,
        //下线审核中...
        [Display(Name = "下线审核中")]
        OfflineAuiting,
        //下线审核被拒绝.
        [Display(Name = "下线审核被拒绝")]
        OfflineAuditRejected
    }
}
