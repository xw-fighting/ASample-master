using System.Collections.Generic;

namespace Thirdparty.Wechat.Service.WxModels
{
    public class WxTemplateDataContainer
    {
        public WxTemplateKeyword First { get; set; }

        public List<WxTemplateKeyword> Data { get; set; }

        public WxTemplateKeyword Remark { get; set; }
    }

    public class WxTemplateKeyword
    {
        public string Color { get; set; }

        public string  Value { get; set; }

        public string Key { get; set; }
    }
}