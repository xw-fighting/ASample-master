using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ThirdParty.IBMWebsphereMQ.Model
{
    public class ReturnResult
    {
        /// <summary>
        /// 是否出错
        /// </summary>
        public  bool IsError { get; set; }

        /// <summary>
        /// IsError 为false 时 为提示信息，为true 时为获取的消息内容
        /// </summary>
        public string Message { get; set; }

        public static ReturnResult Error(string msg)
        {
            return new ReturnResult
            {
                IsError = true,
                Message = msg
            };
        }

        public static ReturnResult Success(string msg)
        {
            return new ReturnResult
            {
                IsError = false,
                Message = msg
            };
        }
    }
}
