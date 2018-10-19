using ASample.Thirdpary.Tencent.Identity.TxInputPara;
using ASample.Thirdpary.Tencent.Identity.TxReslts;
using System.Threading.Tasks;

namespace ASample.Thirdpary.Tencent.Identity
{
    public interface ITxRelNameCheck
    {
        /// <summary>
        /// 签名算法
        /// </summary>
        /// <param name="signaturePara">签名参数</param>
        /// <param name="apiName">调用api名称</param>
        /// <returns></returns>
        TxResult GetSignature(TxSignatureInfo signaturePara, string apiName);

        /// <summary>
        /// 实名登录
        /// </summary>
        /// <param name="signaturePara">签名参数</param>
        /// <param name="userId">用户唯一标识,（如微信的用户openid）</param>
        /// <returns>TxRealNameLoginResult</returns>
        Task<TxRealNameLoginResult> RealNameLogin(TxSignatureInfo signaturePara, string userId);

        /// <summary>
        /// 活体验证码接口
        /// </summary>
        /// <param name="signaturePara">签名参数</param>
        /// <param name="token">凭据由实名登录接口提供</param>
        /// <returns>TxLiveVerificationCodeResult</returns>
        Task<TxLiveVerificationCodeResult> LiveVerificationCode(TxSignatureInfo signaturePara, string token);

        /// <summary>
        /// 活体检测接口
        /// </summary>
        /// <param name="signaturePara">签名参数</param>
        /// <param name="info">输入参数类</param>
        /// <returns>TxLiveCheckResult</returns>
        Task<TxLiveCheckResult> LiveCheck(TxSignatureInfo signaturePara, InputLiveCheck info);

        /// <summary>
        /// 通过腾讯证件库进行活体检测
        /// </summary>
        /// <param name="signaturePara">签名参数</param>
        /// <param name="info">输入参数类</param>
        /// <returns></returns>
        Task<TxLiveCheckResult> LiveCheckByTencent(TxSignatureInfo signaturePara, InputLiveCheck info);

        /// <summary>
        /// 获取活体检测结果信息
        /// </summary>
        /// <param name="signaturePara">签名参数</param>
        /// <param name="token">活体检测凭据</param>
        /// <returns></returns>
        Task<TxLiveCheckInfoResult> GetLiveChenkInfo(TxSignatureInfo signaturePara, string token);


    }
}
