namespace Thirdparty.Wechat.Service.WxModels
{
    /// <summary>
    /// 标签
    /// </summary>
    public class WxTag
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public WxTag(string name)
        {
            Name = name;
            Id = "";
        }
        public WxTag()
        {

        }
    }
}