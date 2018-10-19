using System.Collections.Generic;
using System.Threading.Tasks;
using Thirdparty.Wechat.Service.WxApiConfig;
using Thirdparty.Wechat.Service.WxFilters;
using Thirdparty.Wechat.Service.WxJsonMessages;
using Thirdparty.Wechat.Service.WxModels;
using Thirdparty.Wechat.Service.WxResults;
using DRapid.Utility.Http;
using DRapid.Utility.Log;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class WxUserManager : WxAbstractOperating
    {
        public WxUserManager(WxOption option, ILogger logger)
            : base(option, logger)
        {

        }

        public WxUserManager(WxOption option)
            : base(option)
        {
        }

        /// <summary>
        /// 根据标签获取用户列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<WxUserPage> SelectUserByTagForPage(WxGetUserByTagFilter filter)
        {
            var url = string.Format(WxUserInfoApis.GetUsersByTag, Token);
            var jsonStr = JsonConvert.SerializeObject(new WxJsonGetUserByTagFilter(filter));
            return JsonConvert.DeserializeObject<WxSelectUserByTagResult>(await PostAsync(url, jsonStr, true)).GetUserPage();
        }

        /// <summary>
        /// 批量打上标签
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<WxTagResult> BatchTaggingForUsers(WxBatchFilter filter)
        {
            var url = string.Format(WxTagApis.BatchTagging, Token);
            var jsonStr = JsonConvert.SerializeObject(new WxJsonBatchFilter(filter));
            return JsonConvert.DeserializeObject<WxTagResult>(await PostAsync(url, jsonStr, true));
        }

        /// <summary>
        /// 批量取消标签
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<WxTagResult> BatchUntaggingForUsers(WxBatchFilter filter)
        {
            var url = string.Format(WxTagApis.BatchUntagging, Token);
            var jsonStr = JsonConvert.SerializeObject(new WxJsonBatchFilter(filter));
            return JsonConvert.DeserializeObject<WxTagResult>(await PostAsync(url, jsonStr, true));
        }

        /// <summary>
        /// 获取用户上的标签集合
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public async Task<List<string>> SelectTagForUser(string openId)
        {
            var url = string.Format(WxTagApis.GetidList, Token);
            var jsonStr = JsonConvert.SerializeObject(new WxJsonOpenIdFilter(openId));
            return JsonConvert.DeserializeObject<WxGetidListResult>(await PostAsync(url, jsonStr, true))?.TagIds;
        }

        /// <summary>
        /// 根据openId获取WxUserInfo
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public async Task<WxUserInfo> GetUserByOpenId(string openId)
        {
            var url = string.Format(WxUserInfoApis.GetUserInfo, Token, openId);
            return JsonConvert.DeserializeObject<WxUserInfoResult>(await RequestSender.GetAsync(url))?.GetWxUserInfo();
        }
    }
}
