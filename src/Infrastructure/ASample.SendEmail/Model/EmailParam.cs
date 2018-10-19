using ASample.SendEmail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.SendEmail.Model
{
    public class EmailParam
    {
        public EmailParam()
        {
            FromMail = EamilDefaultConfig.FromMail;
            MSenderUsername = EamilDefaultConfig.MSenderUsername;
            MSenderPassword = EamilDefaultConfig.MSenderPassword;
            MSenderServerHost = EamilDefaultConfig.MSenderServerHost;
            MEnableSsl = EamilDefaultConfig.MEnableSsl;
            MEnablePwdAuthentication = EamilDefaultConfig.MEnablePwdAuthentication;
            BodyEncoding = EamilDefaultConfig.BodyEncoding;
            MSenderPort = EamilDefaultConfig.MSenderPort;
            DisplayName = EamilDefaultConfig.DisplayName;
        }

        /// <summary>
        /// 发送人地址
        /// </summary>
        public string FromMail { get; set; }

        /// <summary>
        /// 发件箱的用户名（即@符号前面的字符串，例如：hello@163.com，用户名为：hello）
        /// </summary>
        public string MSenderUsername { get; set; }

        /// <summary>
        /// 发件箱的密码,注意这个值不是邮箱注册的密码，是邮箱给第三方授权的密码
        /// </summary>
        public string MSenderPassword { get; set; }

        /// <summary>
        /// 发件箱的邮件服务器地址（IP形式或字符串形式均可）
        /// </summary>
        public string MSenderServerHost { get; set; }

        /// <summary>
        /// 是否对邮件内容进行socket层加密传输
        /// </summary>
        public bool MEnableSsl { get; set; }

        /// <summary>
        /// 是否对发件人邮箱进行密码验证
        /// </summary>
        public bool MEnablePwdAuthentication { get; set; }

        /// <summary>
        /// 邮件内容的编码格式
        /// </summary>
        public Encoding BodyEncoding { get; set; }

        /// <summary>
        /// 发送邮件所用的端口号（htmp协议默认为25）
        /// </summary>
        public int MSenderPort { get; set; }

        /// <summary>
        /// 发送人显示名称
        /// </summary>
        public string DisplayName { get; set; }
    }
}
