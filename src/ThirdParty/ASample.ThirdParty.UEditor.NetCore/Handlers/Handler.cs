using Microsoft.AspNetCore.Http;
using ASample.ThirdParty.UEditor.NetCore.Models;

namespace ASample.ThirdParty.UEditor.NetCore.Handlers
{
    public abstract class Handler
    {
        public Handler(HttpContext context)
        {
            this.Request = context.Request;
            this.Response = context.Response;
            this.Context = context;
        }

        public abstract UEditorResult Process();

        public HttpRequest Request { get; private set; }
        public HttpResponse Response { get; private set; }
        public HttpContext Context { get; private set; }
    }
}
