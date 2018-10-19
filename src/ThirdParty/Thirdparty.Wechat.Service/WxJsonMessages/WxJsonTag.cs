using Thirdparty.Wechat.Service.WxModels;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    /// <summary>
    /// 包装过的tag
    /// </summary>
    internal class WxJsonTag
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public WxJsonTag(WxTag tag)
        {
            Id = tag.Id;
            Name =   tag.Name;
        }

        public WxJsonTag() { }
    }

    internal class WxJsonTagContainer
    {
        [JsonProperty("tag")]
        public WxJsonTag WxJsonTag { get; set; }

        public WxJsonTagContainer(WxJsonTag tag)
        {
            WxJsonTag =  tag;
        }

        public WxJsonTagContainer() { }

        public WxJsonTagContainer(WxTag tag)
        {
            WxJsonTag = new WxJsonTag(tag);
        }

        public WxTag GetWxTag()
        {
            return new WxTag
            {
                Id = WxJsonTag.Id,
                Name = WxJsonTag.Name
            };
        }
    }
}