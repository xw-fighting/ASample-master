using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thirdparty.Wechat.Service.WxApiConfig;
using Thirdparty.Wechat.Service.WxJsonMessages;
using Thirdparty.Wechat.Service.WxModels;
using Thirdparty.Wechat.Service.WxResults;
using DRapid.Utility.Http;
using DRapid.Utility.Log;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service
{
    /// <summary>
    /// 标签管理
    /// </summary>
    public class WxTagManager : WxAbstractOperating
    {
        public WxTagManager(WxOption option, ILogger logger)
            : base(option, logger)
        {

        }

        public WxTagManager(WxOption option)
            : base(option)
        {

        }

        /// <summary>
        /// 创建标签
        /// </summary>
        /// <param name="wxTag"></param>
        /// <returns></returns>
        public async Task<WxTag> CreateTag(WxTag wxTag)
        {
            var url = string.Format(WxTagApis.TagCreate, Token);
            var jsonStr = JsonConvert.SerializeObject(new WxJsonTagContainer(wxTag));
            var result = JsonConvert.DeserializeObject<WxJsonTagContainer>(await PostAsync(url, jsonStr));
            return result?.GetWxTag();
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="wxTag"></param>
        /// <returns></returns>
        public async Task<WxTagResult> DeleteTag(WxTag wxTag)
        {
            var noList = new List<string>() { "0", "1", "2" };
            if (wxTag.Id.IsNullOrEmpty() || noList.Contains(wxTag.Id))
            {
                return WxTagResult.CreateTagFail();
            }

            var url = string.Format(WxTagApis.TagDelete, Token);
            var jsonStr = JsonConvert.SerializeObject(new WxJsonTagContainer(wxTag));
            return JsonConvert.DeserializeObject<WxTagResult>(await PostAsync(url, jsonStr));
        }

        /// <summary>
        /// 更新标签
        /// </summary>
        /// <param name="wxTag"></param>
        /// <returns></returns>
        public async Task<WxTagResult> UpdateTag(WxTag wxTag)
        {
            var url = string.Format(WxTagApis.TagUpdate, Token);
            var jsonStr = JsonConvert.SerializeObject(new WxJsonTagContainer(wxTag));
            return JsonConvert.DeserializeObject<WxTagResult>(await PostAsync(url, jsonStr));
        }

        /// <summary>
        /// 获取标签
        /// </summary>
        /// <returns></returns>
        public async Task<List<WxTag>> GetTag()
        {
            var url = string.Format(WxTagApis.TagGet, Token);
            return JsonConvert.DeserializeObject<WxTagListResult>(await RequestSender.GetAsync(url)).Tags;
        }
    }
}