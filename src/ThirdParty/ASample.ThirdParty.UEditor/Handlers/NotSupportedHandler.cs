using ASample.ThirdParty.UEditor.Models;
using ASample.ThirdParty.UEditor.Models.OutResult;
using System.Web;

namespace ASample.ThirdParty.UEditor.Handlers
{
    public class NotSupportedHandler : Handler
    {
        public NotSupportedHandler(HttpContext context)
            : base(context)
        {
        }
        public override UEditorResult Process()
        {
            return new UEditorResult
            {
                State = "action 参数为空或者 action 不被支持。"
            };
        }
    }
}
