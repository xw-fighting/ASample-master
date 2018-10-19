using System.Collections.Generic;
using System.Linq;
using Thirdparty.Wechat.Service.WxModels;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    public class WxJsonTemplateMessage
    {
        [JsonProperty("touser")]
        public string OpenId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("template_id")]
        public string TemplateId { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, WxJsonTemplateKeyword> DataContainer { get; protected set; }

        [JsonProperty("miniprogram")]
        public WxJsonMiniprogram Miniprogram { get; set; }

        public WxJsonTemplateMessage() { }

        public WxJsonTemplateMessage(WxTemplateMessage wxTemplateMessage)
        {
            OpenId = wxTemplateMessage.OpenId;
            Url = wxTemplateMessage.Url;
            TemplateId = wxTemplateMessage.TemplateId;
            if (wxTemplateMessage.MiniProgram != null)
            {
                Miniprogram = new WxJsonMiniprogram
                {
                    AppId = wxTemplateMessage.MiniProgram.AppId,
                    PagePath = wxTemplateMessage.MiniProgram.PagePath
                };
            }
            DataContainer = new Dictionary<string, WxJsonTemplateKeyword>();
            var data = wxTemplateMessage.DataContainer.Data;
            DataContainer.Add("first", new WxJsonTemplateKeyword(wxTemplateMessage.DataContainer.First));
            data.ForEach(k =>
            {
                DataContainer.Add($"{k.Key}", new WxJsonTemplateKeyword(k));
            });
            DataContainer.Add("remark", new WxJsonTemplateKeyword(wxTemplateMessage.DataContainer.Remark));
        }
    }
    public class WxJsonMiniprogram
    {
        [JsonProperty("appid")]
        public string AppId { get; set; }

        [JsonProperty("pagepath")]
        public string PagePath { get; set; }

    }
}