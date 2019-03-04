using System;
using System.Net;

namespace ASample.ThirdParty.WeChat.WeChatPay.Core.HttpException
{
    public class HttpResultException:Exception
    {
        public HttpResultException(HttpStatusCode statusCode,
            string responseText = null)
        {
            StatusCode = statusCode;
            ResponseText = responseText;
        }

        public HttpStatusCode StatusCode { get; set; }

        public string ResponseText { get; set; }
    }
}
