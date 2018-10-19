using ASample.Thirdpary.Tencent.Identity.TxInputPara;
using ASample.Thirdpary.Tencent.Identity.TxJsonInput;
using ASample.Thirdpary.Tencent.Identity.TxReslts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Text;
using DRapid.Utility.Log;

namespace ASample.Thirdpary.Tencent.Identity
{

    /// <summary>
    /// 腾讯实名核身份类
    /// </summary>
    public class TxRelNameCheck : TxAbstractOperating, ITxRelNameCheck
    {
        public TxRelNameCheck(ILogger logger) : base(logger)
        {
        }

        /// <summary>
        /// 签名算法
        /// </summary>
        /// <param name="signaturePara">签名参数</param>
        /// <param name="apiName">调用api名称</param>
        /// <returns></returns>
        public TxResult GetSignature(TxSignatureInfo signaturePara, string apiName)
        {
            try {
                //当前时间戳
               var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTime.Now.Kind);
                var timestamp = Convert.ToInt64((DateTime.Now - start).TotalSeconds);
                //字符串拼接
                var orignal = "a=" + signaturePara.Appid + "&m=" + apiName + "&t=" + timestamp + "&e=" + signaturePara.Expired;
                //HMACSHA1 加密后  再用Base64编码
                HMACSHA1 hmacsha1 = new HMACSHA1();
                hmacsha1.Key = Encoding.ASCII.GetBytes(signaturePara.Sercert);
                byte[] dataBuffer = Encoding.ASCII.GetBytes(orignal);
                byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);
                byte[] tmp = new byte[hashBytes.Length + dataBuffer.Length];
                Buffer.BlockCopy(hashBytes, 0, tmp, 0, hashBytes.Length);
                Buffer.BlockCopy(dataBuffer, 0, tmp, hashBytes.Length, dataBuffer.Length);
                var signature = Convert.ToBase64String(tmp);

                return new TxResult { Code = 0, Msg = signature };
            }
            catch (Exception ex)
            {
                return new TxResult { Code = 1, Msg = "人脸识别签名，" + ex.Message };
            }
        }

        /// <summary>
        /// 实名登录
        /// </summary>
        /// <param name="signaturePara">签名参数</param>
        /// <param name="userId">用户唯一标识,（如微信的用户openid）</param>
        /// <returns></returns>
        public async Task<TxRealNameLoginResult> RealNameLogin(TxSignatureInfo signaturePara,string userId)
        {
            
            try
            {
                //判断传入参数
                if (string.IsNullOrEmpty(userId))
                {
                    return new TxRealNameLoginResult { ErrorCode = -1, ErrorMsg = "人脸识别实名登录，传入参数错误。" };
                }
                //参数定义
                var signatureInfo = GetSignature(signaturePara, TxApiAddressConfig.ApiNameAuth);
                if (signatureInfo.Code == 1)
                {
                    return new TxRealNameLoginResult { ErrorCode = -1, ErrorMsg = signatureInfo.Msg };
                }
                var signKey = Md5Encrypt(signaturePara.Appid + "-" + userId + "-" + signaturePara.SignKey,32);
                TxRealNameLogin inputLogin = new TxRealNameLogin { AppId = signaturePara.Appid, UserId = userId, SignKey = signKey };
                var jsonStr = JsonConvert.SerializeObject(inputLogin);
                var dicSign = new Dictionary<string, string> {
                    {"signature",signatureInfo.Msg}
                };
                //Post请求
                var postResult = await PostAsync(TxApiAddressConfig.RealNameLoginUrl, jsonStr, dicSign, true);

                var result = JsonConvert.DeserializeObject<TxRealNameLoginResult>(postResult);
                return result;
            }
            catch (Exception ex)
            {
                return new TxRealNameLoginResult { ErrorCode = -1, ErrorMsg = "人脸识别实名登录," + ex.Message };
            }
        }

        /// <summary>
        /// 活体验证码接口
        /// </summary>
        /// <param name="signaturePara">签名参数</param>
        /// <param name="token">凭据由实名登录接口提供</param>
        /// <returns></returns>
        public async Task<TxLiveVerificationCodeResult> LiveVerificationCode(TxSignatureInfo signaturePara, string token)
        {

            try
            {
                //判断凭据
                if (string.IsNullOrEmpty(token))
                {
                    return new TxLiveVerificationCodeResult { ErrorCode = -1, ErrorMsg = "人脸识别实名登录凭据为空。" };
                }
                //参数定义
                var signatureInfo = GetSignature(signaturePara,TxApiAddressConfig.ApiNameLiveCode);
                if (signatureInfo.Code == 1)
                {
                    return new TxLiveVerificationCodeResult { ErrorCode = -1, ErrorMsg = signatureInfo.Msg };
                }
                var signKey = Md5Encrypt(signaturePara.Appid + "-" + token + "-" + signaturePara.SignKey, 32);
                var inputVerificationCode = new TxLiveVerificationCode { AppId = signaturePara.Appid, Token = token, Sign = signKey };
                var jsonStr = JsonConvert.SerializeObject(inputVerificationCode);
                var dicSign = new Dictionary<string, string> {
                    {"signature",signatureInfo.Msg}
                };
                //Post请求
                var postResult = await PostAsync(TxApiAddressConfig.LiveVerificationCodeUrl, jsonStr, dicSign, true);

                var result = JsonConvert.DeserializeObject<TxLiveVerificationCodeResult>(postResult);
                return result;
            }
            catch (Exception ex)
            {
                return new TxLiveVerificationCodeResult { ErrorCode = -1, ErrorMsg ="人脸识别验证码，" + ex.Message };
            }
        }

        /// <summary>
        /// 活体检测接口
        /// </summary>
        /// <param name="signaturePara">签名参数</param>
        /// <param name="info">输入参数类</param>
        /// <returns></returns>
        public async Task<TxLiveCheckResult> LiveCheck(TxSignatureInfo signaturePara, InputLiveCheck info)
        {
            try
            {

                //传入参数判断
                if (LiveCheckValidationInputPara(info).ErrorCode == -1)
                {
                    return LiveCheckValidationInputPara(info);
                }

                //参数定义
                var signatureInfo = GetSignature(signaturePara, TxApiAddressConfig.ApiNameLiveDetect);
                if (signatureInfo.Code == 1)
                {
                    return new TxLiveCheckResult { ErrorCode = -1, ErrorMsg = signatureInfo.Msg };
                }
                var signKey = Md5Encrypt(info.Type + "-" + info.ImageContent + "-" + info.ImageType + "-" + info.VideoContent
                    + "-" + info.ValidateData + "-" + info.Level + "-" + signaturePara.Appid 
                    + "-" + info.Token + "-" + signaturePara.SignKey, 32);
                var inputLiveCheck = new TxLiveCheck
                {
                    Type = "2",
                    ImageContent = info.ImageContent,
                    ImageType = info.ImageType,
                    VideoContent = info.VideoContent,
                    ValidateData = info.ValidateData,
                    Level = info.Level,
                    AppId = signaturePara.Appid,
                    Token = info.Token,
                    Sign = signKey
                };
                var jsonStr = JsonConvert.SerializeObject(inputLiveCheck);
                var dicSign = new Dictionary<string, string> {
                    {"signature",signatureInfo.Msg}
                };
                //Post请求
                var postResult = await PostAsync(TxApiAddressConfig.LiveCheckUrl, jsonStr, dicSign, true);

                var result = JsonConvert.DeserializeObject<TxLiveCheckResult>(postResult);
                return result;
            }
            catch (Exception ex)
            {
                return new TxLiveCheckResult { ErrorCode = -1, ErrorMsg ="人脸识别检测，" + ex.Message };
            }
        }


        /// <summary>
        /// 通过腾讯证件库进行活体检测
        /// </summary>
        /// <param name="signaturePara">签名参数</param>
        /// <param name="info">输入参数类</param>
        /// <returns></returns>
        public async Task<TxLiveCheckResult> LiveCheckByTencent(TxSignatureInfo signaturePara, InputLiveCheck info)
        {
            try
            {

                //传入参数判断
                if (LiveCheckValidationInputPara(info).ErrorCode == -1)
                {
                    return LiveCheckValidationInputPara(info);
                }

                //参数定义
                var signatureInfo = GetSignature(signaturePara, TxApiAddressConfig.ApiNameLiveDetect);
                if (signatureInfo.Code == 1)
                {
                    return new TxLiveCheckResult { ErrorCode = -1, ErrorMsg = signatureInfo.Msg };
                }
                var signKey = Md5Encrypt(info.Type + "-" + info.Id + "-" + info.Name + "-" + info.VideoContent
                    + "-" + info.ValidateData + "-" + info.Level + "-" + signaturePara.Appid
                    + "-" + info.Token + "-" + signaturePara.SignKey, 32);
                var inputLiveCheck = new TxLiveCheck
                {
                    Type = info.Type,
                    Id = info.Id,
                    Name = info.Name,
                    VideoContent = info.VideoContent,
                    ValidateData = info.ValidateData,
                    Level = info.Level,
                    AppId = signaturePara.Appid,
                    Token = info.Token,
                    Sign = signKey
                };
                var jsonStr = JsonConvert.SerializeObject(inputLiveCheck);
                var dicSign = new Dictionary<string, string> {
                    {"signature",signatureInfo.Msg}
                };
                //Post请求
                var postResult = await PostAsync(TxApiAddressConfig.LiveCheckUrl, jsonStr, dicSign, true);

                var result = JsonConvert.DeserializeObject<TxLiveCheckResult>(postResult);
                return result;
            }
            catch (Exception ex)
            {
                return new TxLiveCheckResult { ErrorCode = -1, ErrorMsg = "人脸识别检测，" + ex.Message };
            }
        }

        /// <summary>
        /// 获取活体检测结果信息
        /// </summary>
        ///<param name = "signaturePara" > 签名参数 </param >
        /// <param name="token">活体检测凭据</param>
        /// <returns></returns>
        public async Task<TxLiveCheckInfoResult> GetLiveChenkInfo(TxSignatureInfo signaturePara, string token)
        {
            try
            {
                //参数判断
                if (string.IsNullOrEmpty(token))
                {
                    return new TxLiveCheckInfoResult { ErrorCode = -1, ErrorMsg = "人脸识别活体检测结果输入参数错误，凭据不能为空。" };
                }


                //参数定义
                var signatureInfo = GetSignature(signaturePara, TxApiAddressConfig.ApiNameLiveDetectInfo);
                if (signatureInfo.Code == 1)
                {
                    return new TxLiveCheckInfoResult { ErrorCode = -1, ErrorMsg = signatureInfo.Msg };
                }
                var signKey = Md5Encrypt( token + "-" + signaturePara.Appid + "-" + signaturePara.SignKey, 32);
                var inputVerificationCode = new TxLiveCheckInfo()
                {
                    Token = token,
                    AppId = signaturePara.Appid,
                    Sign = signKey
                };
                var jsonStr = JsonConvert.SerializeObject(inputVerificationCode);
                var dicSign = new Dictionary<string, string> {
                    {"signature",signatureInfo.Msg}
                };
                //Post请求
                var postResult = await PostAsync(TxApiAddressConfig.GetLiveCheckInfoUrl, jsonStr, dicSign, true);
                if (postResult.Contains("{}"))
                {
                    postResult = postResult.Replace("{}", "\"\"");
                }
                var result = JsonConvert.DeserializeObject<TxLiveCheckInfoResult>(postResult);
                return result;
            }
            catch (Exception ex)
            {
                return new TxLiveCheckInfoResult() {ErrorCode = -1, ErrorMsg = "人脸识别活体检测结果," + ex.Message};
            }
        }

        /// <summary>
        /// 活体检测输入参数验证
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private TxLiveCheckResult LiveCheckValidationInputPara(InputLiveCheck info)
        {
            try
            {
                if (info.Type != "1" && info.Type != "2")
                {
                    return new TxLiveCheckResult { ErrorCode = -1, ErrorMsg = "传入参数错误，type值只能为1和2。" };
                }
                if (info.Type == "2")
                {
                    if (string.IsNullOrEmpty(info.ImageContent) || (info.ImageType != "0" && info.ImageType != "1"))
                    {
                        return new TxLiveCheckResult { ErrorCode = -1, ErrorMsg = "传入参数错误，请查看type=2需要的参数。" };
                    }
                }
                if (string.IsNullOrEmpty(info.VideoContent))
                {
                    return new TxLiveCheckResult { ErrorCode = -1, ErrorMsg = "传入参数错误，视频内容不能为空。" };
                }
                if (string.IsNullOrEmpty(info.ValidateData))
                {
                    return new TxLiveCheckResult { ErrorCode = -1, ErrorMsg = "传入参数错误，活体验证码不能为空。" };
                }
                if (info.Level != "0" && info.Level != "1" && info.Level != "2")
                {
                    return new TxLiveCheckResult { ErrorCode = -1, ErrorMsg = "传入参数错误，活体检测等级参数错误。" };
                }
                if (string.IsNullOrEmpty(info.Token))
                {
                    return new TxLiveCheckResult { ErrorCode = -1, ErrorMsg = "传入参数错误，凭据不能为空。" };
                }

                return new TxLiveCheckResult { ErrorCode = 0 };
            }
            catch (Exception ex)
            {
                return new TxLiveCheckResult { ErrorCode = -1, ErrorMsg = ex.Message };
            }
        }


        
    }
}

