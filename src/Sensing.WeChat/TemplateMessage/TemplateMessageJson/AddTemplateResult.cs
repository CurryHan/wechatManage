using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.WeChat.TemplateMessage.TemplateMessageJson
{
    public class AddTemplateResult: WxJsonResult
    {
        public string template_id { get; set; }
    }
}
