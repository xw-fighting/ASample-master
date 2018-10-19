using ASample.ThirdParty.UEditor.Models;
using ASample.ThirdParty.UEditor.Models.Configs;
using System.Web;

namespace ASample.ThirdParty.UEditor.Handlers
{
    public static class HandelFactory
    {
        public static Handler GetHandler(string action,HttpContext context)
        {
            switch (action)
            {
                case UploadType.UploadImage:
                    return new UploadHandler(context, new UploadConfig
                    {
                        AllowExtensions = Config.GetStringList("imageAllowFiles"),
                        PathFormat = Config.GetString("imagePathFormat"),
                        SizeLimit = Config.GetInt("imageMaxSize"),
                        UploadFieldName = Config.GetString("imageFieldName")
                    });
                case UploadType.UploadScrawl:
                    return new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = new string[] { ".png" },
                        PathFormat = Config.GetString("scrawlPathFormat"),
                        SizeLimit = Config.GetInt("scrawlMaxSize"),
                        UploadFieldName = Config.GetString("scrawlFieldName"),
                        IsBase64 = true,
                        Base64Filename = "scrawl.png"
                    });
                case UploadType.UploadVideo:
                    return new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = Config.GetStringList("videoAllowFiles"),
                        PathFormat = Config.GetString("videoPathFormat"),
                        SizeLimit = Config.GetInt("videoMaxSize"),
                        UploadFieldName = Config.GetString("videoFieldName")
                    });
                case UploadType.UploadFile:
                    return new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = Config.GetStringList("fileAllowFiles"),
                        PathFormat = Config.GetString("filePathFormat"),
                        SizeLimit = Config.GetInt("fileMaxSize"),
                        UploadFieldName = Config.GetString("fileFieldName")
                    });

                case UploadType.ListImage:
                    return new ListFileHandler(context, Config.GetString("imageManagerListPath"), Config.GetStringList("imageManagerAllowFiles"));
                case UploadType.ListFile:
                    return new ListFileHandler(context, Config.GetString("fileManagerListPath"), Config.GetStringList("fileManagerAllowFiles"));
                case UploadType.CatchImage:
                    return new CrawlerHandler(context);
                default:
                    return new NotSupportedHandler(context);
            }
        }
    }
}
