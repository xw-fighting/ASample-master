using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxResults
{
    /// <summary>
    /// 标签返回结果
    /// </summary>
    public class WxTagResult : WxBaseResult
    {
        public static WxTagResult CreateTagFail()
        {
            return new WxTagResult()
            {
                Errcode = "45058",
                Errmsg = "不能修改0/1/2这三个系统默认保留的标签"
            };
        }
    }
}