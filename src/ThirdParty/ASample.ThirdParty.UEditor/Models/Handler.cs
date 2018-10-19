﻿using ASample.ThirdParty.UEditor.Models.OutResult;
using System.Web;

namespace ASample.ThirdParty.UEditor.Models
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
