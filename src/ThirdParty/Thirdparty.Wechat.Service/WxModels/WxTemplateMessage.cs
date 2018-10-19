namespace Thirdparty.Wechat.Service.WxModels
{
    public class WxTemplateMessage
    {
        public string OpenId { get; set; }

        public string Url { get; set; }
 
        public string TemplateId { get; set; }
  
        public WxTemplateDataContainer DataContainer { get; set; }

        public WxMiniprogram MiniProgram { get; set; }
    }

    public class WxMiniprogram
    {
        public string AppId { get; set; }

        public string PagePath { get; set; }
    }
}