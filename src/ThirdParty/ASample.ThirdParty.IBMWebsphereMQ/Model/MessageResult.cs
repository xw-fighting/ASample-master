namespace ASample.ThirdParty.IBMWebsphereMQ.Model
{
    public class MessageResult
    {
        /// <summary>
        /// 是否出错
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// IsError 为false 时 为提示信息，为true 时为获取的消息内容
        /// </summary>
        public string Message { get; set; }

        public static MessageResult Error(string msg)
        {
            return new MessageResult
            {
                IsError = true,
                Message = msg
            };
        }

        public static MessageResult Success(string msg)
        {
            return new MessageResult
            {
                IsError = false,
                Message = msg
            };
        }
    }
}
