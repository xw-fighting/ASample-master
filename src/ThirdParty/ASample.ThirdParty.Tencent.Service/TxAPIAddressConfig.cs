
namespace ASample.Thirdpary.Tencent.Identity
{
    /// <summary>
    /// 腾讯实名核身接口地址
    /// </summary>
    public class TxApiAddressConfig
    {
        /// <summary>
        /// 腾讯实名登录接口地址
        /// </summary>
        public const string RealNameLoginUrl = "https://iauth.wecity.qq.com/new/cgi-bin/api_auth.php";

        /// <summary>
        /// 腾讯活体认证码接口地址
        /// </summary>
        public const string LiveVerificationCodeUrl = "https://iauth.wecity.qq.com/new/cgi-bin/api_getlivecode.php";

        /// <summary>
        /// 腾讯活体检验接口地址
        /// </summary>
        public const string LiveCheckUrl = "https://iauth.wecity.qq.com/new/cgi-bin/api_livedetectfour.php";

        /// <summary>
        /// 获取活体检测信息地址
        /// </summary>
        public const string GetLiveCheckInfoUrl = "https://iauth.wecity.qq.com/new/cgi-bin/api_getdetectinfo.php"; 

        /// <summary>
        /// 标识唯一的应用
        /// </summary>
        public const string AppId = "1035";

        /// <summary>
        /// 秘钥
        /// </summary>
        public const string SecretKey = "b70d01ec0aeeaef96e8d385f7f0c143a";

        /// <summary>
        /// 实名登录Api名称
        /// </summary>
        public const string ApiNameAuth = "api_auth";

        /// <summary>
        /// 活体验证码Api名称
        /// </summary>
        public const string ApiNameLiveCode = "api_getlivecode";

        /// <summary>
        /// 活体检测Api名称
        /// </summary>
        public const string ApiNameLiveDetect = "api_livedetectfour";

        /// <summary>
        /// 活体检测信息Api名称
        /// </summary>
        public const string ApiNameLiveDetectInfo = "api_getdetectinfo";

        /// <summary>
        /// 有限凭证期（秒）
        /// </summary>
        public const int Expired = 600;

        /// <summary>
        /// 私钥
        /// </summary>
        public const string SignKey = "authkey";

    }
}
