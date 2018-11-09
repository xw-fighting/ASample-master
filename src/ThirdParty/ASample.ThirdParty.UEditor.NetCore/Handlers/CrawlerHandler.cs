using System.Linq;
using Microsoft.AspNetCore.Http;
using ASample.ThirdParty.UEditor.NetCore.Models;

namespace ASample.ThirdParty.UEditor.NetCore.Handlers
{
    public class CrawlerHandler: Handler
    {
        private string[] _sources;
        private Crawler[] _crawlers;
        public CrawlerHandler(HttpContext context) : base(context) { }

        public override UEditorResult Process()
        {
            _sources = Request.Form["source[]"];

            //fixed bug:https://github.com/baiyunchen/UEditor.Core/pull/5
            if (_sources == null || _sources.Length == 0)
            {
                _sources = Request.Query["source[]"];
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
