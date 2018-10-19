using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using DRapid.Utility.Http;
using DRapid.Utility.Serialization;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Thirdparty.Wechat.Service.WxApiConfig;
using Thirdparty.Wechat.Service.WxJsonMessages;
using Thirdparty.Wechat.Service.WxModels;
using Thirdparty.Wechat.Service.WxResults;
using DRapid.Utility.Log;
using DRapid.Utility.Exceptional.WellKnown;

namespace Thirdparty.Wechat.Service
{
    /// <summary>
    /// 上传的API
    /// </summary>
    public class WxUploadManager
    {
        private WxAccessTokenInfo WxAccessTokenInfo { get; }

        private ILogger Logger { get; }

        public WxUploadManager(WxOption option)
        {
            WxAccessTokenInfo = new WxAccessTokenInfo(option.AppId, option.Secret);
        }

        public WxUploadManager(WxOption option, ILogger logger)
        {
            Logger = logger;
            WxAccessTokenInfo = new WxAccessTokenInfo(option.AppId, option.Secret);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileUrl">文件地址</param>
        /// <param name="uploadUrl">上传地址</param>
        /// <returns>返回一个微信服务器的地址</returns>
        private string UploadFile(string fileUrl, string uploadUrl)
        {
            var logger = Logger?.CreateChildLogger("wechat.uploadFile");
            try
            {
                var info = new Info("上传至微信服务器");
                info.AppendInfo("targetUrl", uploadUrl);
                info.AppendInfo("sourceUrl", fileUrl);

                //获取文件
                var request = WebRequest.Create(fileUrl) as HttpWebRequest;
                // ReSharper disable once PossibleNullReferenceException
                request.Method = "GET";
                //获取到的文件流
                var fileStream = request.GetResponse().GetResponseStream();
 
                var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                var beginbytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                var endbytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");

                //文件参数头
                var fileHeader =
                    $"Content-Disposition: form-data; name=\"file\"; filename=\"{Path.GetFileName(fileUrl)}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                var fileHeaderBytes = Encoding.UTF8.GetBytes(fileHeader);

                using (var memoryStram = new MemoryStream())
                {
                    memoryStram.Write(beginbytes, 0, beginbytes.Length);
                    memoryStram.Write(fileHeaderBytes, 0, fileHeaderBytes.Length);
                    var buffer = new byte[1024];
                    var byteRead = 0;
                    while ((byteRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        memoryStram.Write(buffer, 0, byteRead);
                    }
                    fileStream.Close();
                    var stringDict = new Dictionary<string, string> { { "len", "500" }, { "wid", "300" } };
                    stringDict.Select(s =>
                    {
                        var str =
                            $"\r\n--{boundary}\r\nContent-Disposition: form-data; name =\"{s.Key}\"\r\n\r\n{s.Value}\r\n";
                        return Encoding.UTF8.GetBytes(str);
                    }).ToList().Foreach(formitembytes =>
                    {
                    // ReSharper disable once AccessToDisposedClosure
                    memoryStram.Write(formitembytes, 0, formitembytes.Length);
                    });
                    memoryStram.Write(endbytes, 0, endbytes.Length);

                    //tempbuffer
                    memoryStram.Position = 0;
                    var tempBuffer = new byte[memoryStram.Length];
                    memoryStram.Read(tempBuffer, 0, tempBuffer.Length);

                    //请求
                    var wxRequest = WebRequest.Create(uploadUrl) as HttpWebRequest;
                    // ReSharper disable once PossibleNullReferenceException
                    wxRequest.Method = "POST";
                    wxRequest.ContentType = $"multipart/form-data; boundary={boundary}";
                    wxRequest.ContentLength = tempBuffer.Length;
                    var wxRequestStream = wxRequest.GetRequestStream();
                    wxRequestStream.Write(tempBuffer, 0, tempBuffer.Length);
                    wxRequestStream.Close();
                    var wxResponse = wxRequest.GetResponse() as HttpWebResponse;

                    using (var sr = new StreamReader(wxResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        var url = sr.ReadToEnd();
                        info.AppendInfo("result", url);
                        logger.IfNotNull(i => i.Info(info.Message, info));
                        return url;
                    }
                }
            }
            catch (Exception ex)
            {
                var err = new Error(ex, "上传至微信服务器失败");
                err.AppendInfo("targetUrl", uploadUrl);
                err.AppendInfo("sourceUrl", fileUrl);
                err.AppendInfo("result", "");
                logger.IfNotNull(i => i.Error(err.Message, err));
                throw;
            }
        }

        /// <summary>
        /// 上传素材图片
        /// </summary>
        /// <param name="fileUrl">图片地址</param>
        /// <returns></returns>
        public WxUpdateArticleInImageResult UploadArtcleImage(string fileUrl)
        {
            var url = string.Format(WxUploadApis.UploadFileUrl, WxAccessTokenInfo.GetToken());
            var result = UploadFile(fileUrl, url);
            var imageResult = Json.Default.Deserialize<WxUpdateArticleInImageResult>(result);
            if (!imageResult.Url.IsNullOrEmpty())
            {
                return imageResult;
            }
            var error = new Error(null, "上传微信服务器失败");
            error.AppendInfo("result", result);
            throw error;
        }

        /// <summary>
        /// 上传媒体文件
        /// </summary>
        /// <param name="wxMediaType">文件类型</param>
        /// <param name="fileUrl">文件地址</param>
        /// <returns>media_id</returns>
        public WxUpdateMediaResult UploadMedia(WxMediaType wxMediaType, string fileUrl)
        {
            var type = "image";
            switch (wxMediaType)
            {
                case WxMediaType.Image:
                    type = "image";
                    break;
                case WxMediaType.Thumb:
                    type = "thumb";
                    break;
                case WxMediaType.Video:
                    type = "video";
                    break;
                case WxMediaType.Voice:
                    type = "voice";
                    break;
            }
            var url = string.Format(WxUploadApis.UploadMediaFileUrl, WxAccessTokenInfo.GetToken(), type);
            var result = UploadFile(fileUrl, url);
            var mediaResult = wxMediaType == WxMediaType.Thumb ? Json.Default.Deserialize<WxUpdateThumbResult>(result) : Json.Default.Deserialize<WxUpdateMediaResult>(result);

            if (!mediaResult.MediaId.IsNullOrEmpty())
            {
                return mediaResult;
            }
            var error = new Error(null, "上传微信服务器失败");
            error.AppendInfo("result", result);
            throw error;
        }

        /// <summary>
        /// 上传图文消息素材【订阅号与服务号认证后均可用】
        /// thumb_media_id:图文消息缩略图的media_id，可以在基础支持-上传多媒体文件接口中获得
        /// </summary>
        /// <param name="articles">图文消息，一个图文消息支持1到10条图文</param>
        /// <returns>success: { "type":"news","media_id":"CsEf3ldqkAYJAU6EJeIkStVDSvffUJ54vqbThMgplD-VJXXof6ctX5fI6-aYyUiQ", 
        /// "created_at":1391857799}</returns>
        public WxUpdateMediaResult UploadArtcles(IEnumerable<WxArtcle> articles)
        {
            var url = string.Format(WxUploadApis.UploadArticleUrl, WxAccessTokenInfo.GetToken());
            var str = JsonConvert.SerializeObject(new WxJsonArticlesContainer(articles.Select(s => new WxJsonArtcle(s))));
            string result;
            var logger = Logger?.CreateChildLogger("wechat.uploadFile");
            try
            {
                HttpStatusCode code;
                result = RequestSender.Post(url, new StringContent(str), out code);
                var info = new Info("上传至微信服务器");
                info.AppendInfo("param", str);
                info.AppendInfo("targetUrl", url);
                info.AppendInfo("result", result);
                logger?.Info(info.Message, info);
            }
            catch (Exception ex)
            {
                var err = new Error(ex, "上传至微信服务器失败");
                err.AppendInfo("param", str);
                err.AppendInfo("targetUrl", url);
                err.AppendInfo("result", "");
                logger?.Error(err.Message, err);
                throw;
            }

            var mediaResult = JsonConvert.DeserializeObject<WxUpdateMediaResult>(result);
            if (!mediaResult.MediaId.IsNullOrEmpty())
            {
                return mediaResult;
            }

            var error = new Error(null, "上传微信服务器失败");
            error.AppendInfo("result", result);
            throw error;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="articles"></param>
        /// <returns></returns>
        public WxUpdateMediaResult UploadArtcles(WxArtcle articles)
        {
            return UploadArtcles(new List<WxArtcle> { articles });
        }

        /// <summary>
        /// 下载素材，返回流
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public async Task<WxDownBaseArticleResult> DownArtcles(string mediaId)
        {
            var url = string.Format(WxUploadApis.DownArticleUrl, WxAccessTokenInfo.GetToken(), mediaId);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var wxDownBaseArticleResult = new WxDownBaseArticleResult();
                if (response.Content.Headers.ContentType.MediaType == "text/plain" ||
                    response.Content.Headers.ContentType.MediaType == "application/json")
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    var baseResult = JsonConvert.DeserializeObject<WxBaseResult>(responseText);
                    wxDownBaseArticleResult.WxResult = baseResult;
                    if (Logger != null)
                    {
                        var logger = Logger.CreateChildLogger("wechat.getVideoStream");
                        var err = new Error(null, "从微信服务器获取视频流失败");
                        err.AppendInfo("mediaType", response.Content.Headers.ContentType.MediaType);
                        err.AppendInfo("mediaId", mediaId);
                        err.AppendInfo("url", url);
                        err.AppendInfo("errcode", baseResult.Errcode);
                        err.AppendInfo("errmsg", baseResult.Errmsg);
                        err.AppendInfo("result", responseText);
                        logger.IfNotNull(i => i.Error(err.Message, err));
                    }

                }
                else
                {
                    wxDownBaseArticleResult.WxResult = WxBaseResult.Succes();
                    wxDownBaseArticleResult.ArticleStream = await response.Content.ReadAsStreamAsync();
                }
                return wxDownBaseArticleResult;
            }
        }

        /// <summary>
        /// 将文本里的图片替换为上传到微信那边的地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string FormatImage(string str)
        {
            var imgRegex = "<img[^>]*src=[\"']*([^>\"']+)[\"']*\\s*[^.]*\\s*[/]?>";
            var regex = new Regex(imgRegex);
            var matchs = regex.Matches(str);
            if (matchs.Count == 0)
            {
                return str;
            }
            var dic = new Dictionary<string, string>();
            foreach (var match in matchs)
            {
                var value = regex.Replace(match.ToString(), "$1");
                var result = UploadArtcleImage(value);
                //if (result.Url.IsNullOrWhiteSpace())
                //{
                //    continue;
                //}
                dic.Add(value, result.Url);
            }
            var html = dic.Aggregate(str, (current, d) => new Regex(d.Key).Replace(current, d.Value));
            return html;
        }
    }
}