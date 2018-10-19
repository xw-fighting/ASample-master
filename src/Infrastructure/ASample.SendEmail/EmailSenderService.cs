using ASample.SendEmail.Model;
using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace ASample.SendEmail
{
    public class EmailSenderService
    {
        private MailMessage MMailMessage;   //主要处理发送邮件的内容（如：收发人地址、标题、主体、图片等等）
        private SmtpClient MSmtpClient; //主要处理用smtp方式发送此邮件的配置信息（如：邮件服务器、发送端口号、验证方式等等）

        public EamilMessage EamilMessage { get; set; }

        public EmailParam MaitSender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eamilMessage">消息内容</param>
        public EmailSenderService(EamilMessage eamilMessage)
        {
            SetEmail(eamilMessage, new Model.EmailParam());
        }

        /// <summary>
        /// 设置邮件
        /// </summary>
        /// <param name="eamilMessage">消息内容</param>
        public void SetEmail(EamilMessage eamilMessage)
        {
            SetEmail(eamilMessage, new Model.EmailParam());
        }
        /// <summary>
        /// 设置邮件
        /// </summary>
        /// <param name="eamilMessage">消息内容</param>
        /// <param name="maitSender">发送配置</param>
        public void SetEmail(EamilMessage eamilMessage, Model.EmailParam maitSender)
        {
            EamilMessage = eamilMessage;
            MaitSender = maitSender;

            MMailMessage = new MailMessage();
            MMailMessage.To.Add(eamilMessage.ToMail);

            MMailMessage.From = new MailAddress(MaitSender.FromMail, eamilMessage.DisplayName ?? MaitSender.DisplayName);
            MMailMessage.Subject = eamilMessage.Subject;
            MMailMessage.Body = eamilMessage.EmailBody;
            MMailMessage.IsBodyHtml = eamilMessage.IsHtml;
            MMailMessage.BodyEncoding = MaitSender.BodyEncoding;
            MMailMessage.Priority = eamilMessage.Priority ?? MailPriority.Normal;
        }

        ///<summary>
        /// 添加附件
        ///</summary>
        ///<param name="attachmentsPath">附件的路径集合，以分号分隔</param>
        public void AddAttachments(string attachmentsPath)
        {
            try
            {
                string[] path = attachmentsPath.Split(';'); //以什么符号分隔可以自定义
                Attachment data;
                ContentDisposition disposition;
                for (int i = 0; i < path.Length; i++)
                {
                    data = new Attachment(path[i], MediaTypeNames.Application.Octet);
                    disposition = data.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(path[i]);
                    disposition.ModificationDate = File.GetLastWriteTime(path[i]);
                    disposition.ReadDate = File.GetLastAccessTime(path[i]);
                    MMailMessage.Attachments.Add(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        ///<summary>
        /// 邮件的发送
        ///</summary>
        public void Send()
        {
            try
            {
                if (MMailMessage != null)
                {
                    MSmtpClient = new SmtpClient(MaitSender.MSenderServerHost);
                    //mSmtpClient.Host = "smtp." + mMailMessage.From.Host;
                    //mSmtpClient.Host = this.mSenderServerHost;
                    MSmtpClient.Port = MaitSender.MSenderPort;
                    MSmtpClient.UseDefaultCredentials = false;
                    MSmtpClient.EnableSsl = MaitSender.MEnableSsl;
                    if (MaitSender.MEnablePwdAuthentication)
                    {
                        System.Net.NetworkCredential nc = new System.Net.NetworkCredential(MaitSender.MSenderUsername, MaitSender.MSenderPassword);

                        MSmtpClient.Credentials = nc.GetCredential(MSmtpClient.Host, MSmtpClient.Port, "NTLM");
                    }
                    else
                    {
                        MSmtpClient.Credentials = new System.Net.NetworkCredential(MaitSender.MSenderUsername, MaitSender.MSenderPassword);
                    }
                    MSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    MSmtpClient.Send(MMailMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
