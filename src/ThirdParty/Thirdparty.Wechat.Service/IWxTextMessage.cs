namespace Thirdparty.Wechat.Service
{
    public interface IWxTextMessage : IWxMessage
    {
        /// <summary>
        /// 内容
        /// </summary>
        string Content { get; set; }
    }
}