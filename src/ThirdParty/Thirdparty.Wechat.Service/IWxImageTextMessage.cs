namespace Thirdparty.Wechat.Service
{
    public interface IWxImageTextMessage : IWxMessage
    {
        string MediaId { get; set; }

        /// <summary>
        /// 原创校验
        /// </summary>
        int Reprint { get; set; }
    }
}