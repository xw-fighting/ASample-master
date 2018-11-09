using Microsoft.AspNetCore.Http;
using ASample.ThirdParty.UEditor.NetCore.Models;

namespace ASample.ThirdParty.UEditor.NetCore.Handlers
{
    public class NotSupportedHandler:Handler
    {
        public NotSupportedHandler(HttpContext context): base(context)
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
