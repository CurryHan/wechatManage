﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.Core
{
    public class MailSection : ConfigurationSection
    {
        /// <summary>
        /// 注册时是否需要验证邮箱
        /// </summary>
        [ConfigurationProperty("requireValid", DefaultValue = "false", IsRequired = true)]
        public bool RequireValid
        {
            get { return (bool) this["requireValid"]; }
            set { this["requireValid"] = value; }
        }

        /// <summary>
        /// SMTP服务器
        /// </summary>
        [ConfigurationProperty("server", IsRequired = false)]
        public string Server
        {
            get { return (string) this["server"]; }
            set { this["server"] = value; }
        }

        /// <summary>
        /// 默认端口25（设为-1让系统自动设置）
        /// </summary>
        [ConfigurationProperty("port", DefaultValue = "25", IsRequired = false)]
        public int Port
        {
            get { return (int) this["port"]; }
            set { this["port"] = value; }
        }

        /// <summary>
        /// 账号
        /// </summary>
        [ConfigurationProperty("uid", IsRequired = false)]
        public string Uid
        {
            get { return (string) this["uid"]; }
            set { this["uid"] = value; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        [ConfigurationProperty("password", IsRequired = false)]
        public string Pwd
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }

        /// <summary>
        /// 是否使用SSL连接
        /// </summary>
        [ConfigurationProperty("enableSSL", DefaultValue = "false", IsRequired = false)]
        public bool EnableSSL
        {
            get { return (bool) this["enableSSL"]; }
            set { this["enableSSL"] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("enablePwdCheck", DefaultValue = "false", IsRequired = false)]
        public bool EnablePwdCheck
        {
            get { return (bool) this["enablePwdCheck"]; }
            set { this["enablePwdCheck"] = value; }
        }
    }
}
