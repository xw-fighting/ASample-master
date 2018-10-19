using System.Configuration;
using qcloudsms_csharp;

namespace Thirdparty.Tencent.SMS
{
    public class SmsSend
    {
        private static SmsSingleSender Sender;

       
        static SmsSend()
        {
            Sender = new SmsSingleSender(1400063547, "");
        }

        private static bool IsSend
        {
            get
            {
                var isSendSmsStr = ConfigurationManager.AppSettings["IsSendSms"];
                // ReSharper disable once RedundantAssignment
                var i = 0;
                int.TryParse(isSendSmsStr, out i);
                return i == 1;
            }
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        public static SmsSingleSenderResult SendCode(string phone, string code)
        {
            //发送验证码，不能发送短信的时候返回成功
            return IsSend
                ? SendResult(phone, new[] {code}, SmsTemplate.Code)
                : new SmsSingleSenderResult {errMsg = "发送短信已关闭，请在配置中开启", result = 0};
        }

        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public static SmsSingleSenderResult SendMessage(string phone, string message)
        {
            return SendResult(phone, new[] {message}, SmsTemplate.Notice);
        }

        /// <summary>
        /// 发送模版消息
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="message">消息</param>
        /// <param name="smsTemplate">消息模版</param>
        /// <returns></returns>
        public static SmsSingleSenderResult SendTemplateMessage(string phone, string message, SmsTemplate smsTemplate)
        {
            return SendResult(phone, new[] {message}, smsTemplate);
        }

        /// <summary>
        /// 发送审核结果消息
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="parameters">消息</param>
        /// <param name="smsTemplate">消息模版</param>
        /// <returns></returns>
        public static SmsSingleSenderResult SendResult(string phone, string[] parameters, SmsTemplate smsTemplate)
        {
            return IsSend
                ? Sender.sendWithParam("86", phone, (int) smsTemplate, parameters, "", "", "")
                : new SmsSingleSenderResult {errMsg = "发送短信已关闭，请在配置中开启", result = 90001};
        }
    }

    public enum SmsTemplate
    {
        /// <summary>
        /// 信息提交
        /// </summary>
        InfoSubmit = 95855,

        /// <summary>
        /// 验证码
        /// </summary>
        Code = 77924,

        /// <summary>
        /// 通知
        /// </summary>
        Notice = 77924,

        /// <summary>
        /// 开发区审核结果
        /// </summary>
        Result = 100212,

        /// <summary>
        /// 民办初中信息提交
        /// </summary>
        MBInfoSubmit = 107722
    }
}