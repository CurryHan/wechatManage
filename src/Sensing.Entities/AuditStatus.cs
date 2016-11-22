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
        [Display(Name ="初始状态")]
        None=0,
        //当前设备没有入组,处于
        [Display(Name = "下线")]
        Offline=1,
        //上线审核中...
        [Display(Name = "上线审核中")]
        OnlineAuditing=2,
        //审核通过,在线
        [Display(Name = "上线")]
        Online=3,
        //上线审核被拒绝.
        [Display(Name = "上线审核被拒绝")]
        OnlineAuditRejected=4,
        //下线审核中...
        [Display(Name = "下线审核中")]
        OfflineAuiting=5,
        //下线审核被拒绝.
        [Display(Name = "下线审核被拒绝")]
        OfflineAuditRejected=6
    }
}
