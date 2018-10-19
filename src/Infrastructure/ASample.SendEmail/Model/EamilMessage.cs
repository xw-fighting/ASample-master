
using System.Net.Mail;

namespace ASample.SendEmail.Model
{
    public class EamilMessage
    {
        public EamilMessage()
        {
            if (Priority == null) Priority = MailPriority.Normal;
        }

        /// <summary>
        /// 发送人显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 收件人地址（可以是多个收件人，程序中是以“;"进行区分的）
        /// </summary>
        public string ToMail { get; set; }

        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 邮件内容（可以以html格式进行设计）
        /// </summary>
        public string EmailBody { get; set; }

        /// <summary>
        /// body内容是否为html
        /// </summary>
        public bool IsHtml { get; set; }

        /// <summary>
        /// 邮件优先级
        /// </summary>
        public MailPriority? Priority { get; set; }
    }
}
