/// <summary>
/// 状态码路由管理器
/// </summary>
/// <param name="infoAction">消息处理器</param>
/// <param name="causionAction">警告处理器</param>
/// <param name="errorAction">错误处理器</param>
/// <param name="seriousErrorAction">严重错误处理器</param>
/// <param name="loginAction">身份过期处理器</param>
/// <param name="redirectAction">跳转处理器</param>
/// <param name="notImplementedAction">未实现</param>
/// <param name="notFoundAction">未找到此方法</param>
/// <param name="forbiddenAction">不被允许，没有权限</param>
/// <param name="requestErrorAction">请求错误</param>
/// <returns></returns>
function ManagedStatusRouter(actions) {
    this.InfoAction = actions['infoAction'];
    this.CausionAction = actions['causionAction'];
    this.ErrorAction = actions['errorAction'];
    this.LoginAction = actions['loginAction'];
    this.SeriousErrorAction = actions['seriousErrorAction'];
    this.RedirectAction = actions['redirectAction'];
    if (actions['notFoundAction']) {
        this.NotFoundAction = actions['notFoundAction'];
    } else {
        this.NotFoundAction = actions['seriousErrorAction'];
    }
    if (actions['requestErrorAction']) {
        this.RequestErrorAction = actions['requestErrorAction'];
    } else {
        this.RequestErrorAction = actions['seriousErrorAction'];
    }
    if (actions['notImplementedAction']) {
        this.NotImplementedAction = actions['notImplementedAction'];
    } else {
        this.NotImplementedAction = actions['seriousErrorAction'];
    }
    if (actions['forbiddenAction']) {
        this.ForbiddenAction = actions['forbiddenAction'];
    } else {
        this.ForbiddenAction = actions['errorAction'];
    }

    //路由
    this.route = function (jqXhr, managedStatus) {
        if (!managedStatus) {
            managedStatus = xhrObjectHelper.getManagedCode(jqXhr);
        }
        var data= xhrObjectHelper.getResponseData(jqXhr);
        if (managedStatus === "0") {
            this.RequestErrorAction(jqXhr,data);
            return true;
        }
        if (managedStatus === "298") {
            this.InfoAction(jqXhr, data);
            return true;
        }
        if (managedStatus === "299") {
            this.CausionAction(jqXhr, data);
            return true;
        }
        if (managedStatus === "400") {
            this.ErrorAction(jqXhr, data);
            return true;
        }
        if (managedStatus === "500") {
            this.SeriousErrorAction(jqXhr, data);
            return true;
        }
        if (managedStatus === "302") {
            this.RedirectAction(jqXhr, data);
            return true;
        }
        if (managedStatus === "501") {
            this.NotImplementedAction(jqXhr, data);
            return true;
        }
        if (managedStatus === "404") {
            this.NotFoundAction(jqXhr, data);
            return true;
        }
        if (managedStatus === "401") {
            this.LoginAction(jqXhr, data);
            return true;
        }
        if (managedStatus === "403") {
            this.ErrorAction(jqXhr, data);
            return true;
        }
        return false;
    };
}

//xhr解析帮助方法
var xhrObjectHelper = {
    //Code
    managedStatusHeaderKey: "ManagedStatusCode",

    //获取Code
    getManagedCode: function (xhrObject) {
        var code = xhrObject.getResponseHeader(xhrObjectHelper.managedStatusHeaderKey);
        if (code == null) {
            code = xhrObject.status.toString();
        }
        return code;
    },

    //获取Data
    getResponseData: function(xhrObject) {
        var text = xhrObject.responseText;
        if (xhrObjectHelper.isTextEmptyOrWhitespace(text)) {
            /*如果无法在正文中找到相关内容，那么去请求头中找*/
            text = xhrObject.getResponseHeader("X-Responded-JSON");
        }
        if (xhrObjectHelper.isTextEmptyOrWhitespace(text)) {
            /*302响应会将地址放在location中*/
            var loaction = xhrObject.getResponseHeader("Location");
            if (xhrObjectHelper.isTextEmptyOrWhitespace(loaction)) {
                return null;
            }
            return { redirectUrl: location };
        }
        try {
            return eval("(" + text + ")");
        } catch (ex) {
            return null;
        }
    },
    isTextEmptyOrWhitespace: function (text) {
        return (text == null || text === '' || text.replace(/(^s*)|(s*$)/g, "").length === 0);
    }
};