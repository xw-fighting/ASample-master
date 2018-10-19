using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.SendEmail.Model
{
    public class EamilDefaultConfig
    {
        /// <summary>
        /// 发送邮件所用的端口号（htmp协议默认为25）
        /// </summary>
        public static int MSenderPort = 25;

        /// <summary>
        /// 发件箱的邮件服务器地址（IP形式或字符串形式均可）
        /// </summary>
        public static string MSenderServerHost = "smtp.163.com";

        /// <summary>
        /// 发送人地址
        /// </summary>
        public static string FromMail = "18079605966@163.com";

        /// <summary>
        /// 发件箱的用户名（即@符号前面的字符串，例如：hello@163.com，用户名为：hello）
        /// </summary>
        public static string MSenderUsername = "18079605966@163.com";

        /// <summary>
        /// 发件箱的密码,注意这个值不是邮箱注册的密码，是邮箱给第三方授权的密码
        /// </summary>
        public static string MSenderPassword = "1qaz2wsx";

        /// <summary>
        /// 是否对邮件内容进行socket层加密传输
        /// </summary>
        public static bool MEnableSsl = true;

        /// <summary>
        /// 是否对发件人邮箱进行密码验证
        /// </summary>
        public static bool MEnablePwdAuthentication = true;

        /// <summary>
        /// 邮件内容的编码格式
        /// </summary>
        public static Encoding BodyEncoding = Encoding.UTF8;

        /// <summary>
        /// 发送人显示名称
        /// </summary>
        public static string DisplayName = "xiaowei";

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="mSenderPort">发送邮件所用的端口号（htmp协议默认为25）</param>
        /// <param name="mSenderServerHost">发件箱的邮件服务器地址（IP形式或字符串形式均可）</param>
        /// <param name="mSenderUsername">发送人地址</param>
        /// <param name="mSenderPassword">发件箱的用户名（即@符号前面的字符串，例如：hello@163.com，用户名为：hello）</param>
        /// <param name="displayName">发件箱的密码,注意这个值不是邮箱注册的密码，是邮箱给第三方授权的密码</param>
        /// <param name="mEnableSsl">是否对邮件内容进行socket层加密传输</param>
        /// <param name="mEnablePwdAuthenticatio">是否对发件人邮箱进行密码验证</param>
        /// <param name="bodyEncoding">邮件内容的编码格式</param>
        public static void Init(string fromMail, string mSenderServerHost, string mSenderUsername, string mSenderPassword, string displayName = null, int mSenderPort = 25, bool mEnableSsl = true, bool mEnablePwdAuthenticatio = true, Encoding bodyEncoding = null)
        {
            FromMail = fromMail;
            MSenderPort = mSenderPort;
            MSenderServerHost = mSenderServerHost;
            MSenderUsername = mSenderUsername;
            MSenderPassword = mSenderPassword;
            MEnableSsl = mEnableSsl;
            MEnablePwdAuthentication = mEnablePwdAuthenticatio;
            BodyEncoding = bodyEncoding ?? Encoding.UTF8;
            DisplayName = displayName;
        }
    }
}
