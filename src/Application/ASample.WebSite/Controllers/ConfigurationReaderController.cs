using ASample.Configuration;
using ASample.Serialize.XmlSerialize;
using ASample.WebSite.Models.ConfigurationReader;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ASample.WebSite.Controllers
{
    public class ConfigurationReaderController : Controller
    {
        // GET: ConfigurationReader
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 读取XML文件输出字符串
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string ReadXmlContent()
        {
            var xmlStr = ConfigurationReader.Read<StorageConfig>();
            return xmlStr;
        }

        /// <summary>
        /// 读取XML文件将XML 字符串转换为指定对象
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult XmlDeserialize()
        {
            //读取XML文件转换为对象
            var config = ConfigurationReader.Read<StorageConfig>(new XmlSerialize());
            var result = new StorageConfig{
                ManageSite = config.ManageSite,
                ManageSitePhyPath = config.ManageSitePhyPath,
                MobileSite = config.MobileSite,
                UploadImagePath = config.UploadImagePath,
                
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}