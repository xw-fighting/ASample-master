using ASample.ThirdParty.UEditor.Models;
using ASample.ThirdParty.UEditor.Models.OutResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ASample.ThirdParty.UEditor.Handlers
{
    public class CrawlerHandler : Handler
    {
        private string[] _sources;
        private Crawler[] _crawlers;
        public CrawlerHandler(HttpContext context):base(context)
        {

        }
        public override UEditorResult Process()
        {
            _sources = Request.Form.GetValues("source[]");

            //fixed bug:https://github.com/baiyunchen/UEditor.Core/pull/5
            if (_sources == null || _sources.Length == 0)
            {
                _sources = Request.QueryString.GetValues("source[]");
            }

            if (_sources == null || _sources.Length == 0)
            {
                return new UEditorResult
                {
                    State = "参数错误：没有指定抓取源"
                };

            }
            _crawlers = _sources.Select(x => new Crawler(x).Fetch()).ToArray();
            return new UEditorResult
            {
                State = "SUCCESS",
                List = _crawlers.Select(x => new UEditorFileList
                {
                    State = x.State,
                    Source = x.SourceUrl,
                    Url = x.ServerUrl
                })
            };
        }


    }
}
