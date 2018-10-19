/*基础功能库*/
$.extend({
    //json数据转为Url
    jsonToUrl: function (object) {
        var url = '';
        for (var field in object) {
            url += field + '=' + (object[field] == null ? "" : object[field]) + '&';
        }
        return url.substring(0, url.length - 1);
    },

    //json数据转化为url
    combineUrl: function (url, data) {
        if (data) {
            var combineUrl;
            if (url.indexOf('?') > 0) {
                combineUrl = url;
                if (combineUrl.substring(combineUrl.length - 1, combineUrl.length) != '&') {
                    combineUrl += '&';
                }
            } else {
                combineUrl = url + '?';
            }
            var queryString = $.jsonToUrl(data);
            combineUrl += queryString;
            return combineUrl;
        }
        return url;
    },

    //绑定toolbar事件
    bindToolEvent: function (btns, methodO) {
        btns.each(function (i) {
            var currentBtn = $(btns[i]);
            var id = currentBtn.attr('id');
            if (id) {
                var fnName = id.replace('btn', '');
                var firstLetter = fnName.substr(0, 1).toLowerCase();
                fnName = firstLetter + fnName.substring(1);
                if (methodO && methodO.hasOwnProperty(fnName)) {
                    currentBtn.click(methodO[fnName]);
                }
            }
        });
    },

    //打包Form或者查询的data
    packData: function (selector) {
        var elements = $(selector + " [name]");
        var data = {};
        for (var i = 0; i < elements.length; i++) {
            var el = $(elements[i]);
            var name = el.attr('name');
            var value = el.val();
            if (typeof (value) === 'string') {
                value = $.trim(el.val());
            }
            //select多选的数组
            if ($.isArray(value)) {
                value = value.join(',');
            }
            if (el.is('input') && el.attr('type') === 'checkbox') {
                if (el.prop('checked')) {
                    if (data[name]) {
                        data[name] += ',' + value;
                    } else {
                        data[name] = value;
                    }
                }
                continue;
            }
            data[name] = value;
        }
        return data;
    },

    //清空Form
    clearForm: function (canvas) {
        canvas.find('input').each(function (i, item) {
            $(item).val('');
        });
        canvas.find('textarea').val('');
    },

    //自动通过rows赋值form(通过name)
    loadRowForm: function (selector, data) {
        $.each(data, function (key, item) {
            if (typeof (item) === 'object')
            {
                return;
            }

            var name = key.substr(0, 1).toUpperCase() + key.substr(1) + "";
            var control = $(selector + ' [name="' + name + '"]');
            if (control.length === 0) {
                return true;
            }
            control.val(item);
        });
    },

    //自动赋值form(通过name)
    loadForm: function (selector, data) {
        $.each(data, function (key, item) {
            if (item != null && typeof (item) === 'object' && !$.isArray(item)) {
                $.loadForm(selector, item);
            }
            var name = key.substr(0, 1).toUpperCase() + key.substr(1) + "";
            var control = $(selector + ' [name="' + name + '"]');
            if (control.length === 0) {
                return true;
            }
            //图片
            if (control[0].localName === "img") {
                control.attr('src', item);
                return true;
            }
            control.val(item);
            if (control[0].localName !== "select" && control[0].localName !== 'div' && control[0].localName !== 'img') {
                control.html(item);
            }
        });
    },

    //货币格式为千位分割
    moneyFormat: function (s) {
        if (/[^0-9\.]/.test(s)) return "invalid value";
        s = s.replace(/^(\d*)$/, "$1.");
        s = (s + "00").replace(/(\d*\.\d\d)\d*/, "$1");
        s = s.replace(".", ",");
        var re = /(\d)(\d{3},)/;
        while (re.test(s))
            s = s.replace(re, "$1,$2");
        s = s.replace(/,(\d\d)$/, ".$1");
        return s.replace(/^\./, "0.");
    }
});

//Ajax动态加载库
$.extend({
    //包装后的Ajax
    ajaxPost: function (url, params, successFn, msg, useJson, option, isNoMsg) {
        params = params || {};
        if (!isNoMsg) {
            msg = msg ? msg : '请稍后...';
            $.loading(msg);
        }
        //成功，完成，错误回掉不支持自定义
        if (option) {
            delete option['complete'];
            delete option['success'];
            delete option['error'];
        }
        var defaultOption = {
            url: url,
            type: 'POST',
            data: params,
            complete: function (xhr, ts) {
                xhr = null;
                return;
            },
            //200成功
            success: function (data, textStatus, jqXhr) {
                $.hideLoading(msg);
                var isFail = defaultStatusRouter.route(jqXhr);
                //回调
                if (!isFail && successFn) {
                    successFn(data);
                }
            },
            //错误
            error: function (data, textStatus, jqXHR) {
                $.hideLoading(msg);
                var message = '系统出错了！';
                if (data && data.message) {
                    message = data.message;
                }
                $.msgError(message);
            }

        };
        if (useJson) {
            defaultOption.contentType = "application/json";
            defaultOption.data = JSON.stringify(params);
        }
        option = $.extend(defaultOption, option);
        return $.ajax(option);
    }
});

//--------------------打开窗体，关闭窗体，预览图片   start---------------------------
//打开窗体
$.fn.openForm = function (titile, formWidth, formTop) {
    var content = $(this);
    //清空验证错误
    content.clearValidError();
    //宽度
    if (!formWidth) {
        formWidth = $(window).width() * 0.6;
        formWidth = formWidth < 500 ? 500 : formWidth;
        formWidth = formWidth > 900 ? 900 : formWidth;
    }
    var config = {
        type: 1,
        title: titile,
        scrollbar: false,
        closeBtn: 1,
        zIndex: 9000,
        area: formWidth + 'px',
        content: content
    };
    //offset的top
    if (formTop) {
        config['offset'] = formTop;
    }
    var index = layer.open(config);
    //找到layui-layer-content
    var maxHeight = $(window).height() - 42;
    if (formTop) {
        maxHeight = maxHeight - formTop;
    }
    content.closest('.layui-layer-content').css('max-height', maxHeight);
    return index;
};

//关闭窗体
$.fn.closeForm = function (index) {
    if (index) {
        layer.close(index);
    }
    else {
        layer.closeAll('page');
    }
};

$.extend({
    //打开看到图片详情
    openImageForm: function (src) {
        //宽度
        var formWidth = $(window).width() * 0.6;
        formWidth = formWidth < 500 ? 500 : formWidth;
        formWidth = formWidth > 900 ? 900 : formWidth;
        var formHeight = $(window).height() - 200;
        //自适应图片
        var imgtemp = $(new Image());
        imgtemp.load(function () {
            var imgW = this.width;
            var imgH = this.height + 43;
            formWidth = imgW > formWidth ? formWidth : imgW;
            formHeight = imgH > formHeight ? formHeight : imgH;
            var areaWidth = formWidth > 160 ? formWidth : 160; //设置默认宽度
            var content = '<div style="overflow: hidden;"><img src="' + src + '" style="width:' + formWidth + 'px;height:auto;display: block;margin-left:auto;margin-right:auto;"/></div>';
            layer.open({
                type: 1,
                title: '图片详情',
                scrollbar: false,
                area: [areaWidth + 'px', formHeight + 'px'],
                maxWidth: 900,
                offset: '130px',
                content: content
            });
        });
        imgtemp.error(function () {
            $.msgAlert('图片打开失败！');
        });
        imgtemp.attr('src', src);

    }
});
//--------------------图片自适应   end---------------------------


//--------------------图片自适应   start---------------------------
$.fn.imageAdaption = function () {
    $(this).each(function () {
        var frame = $(this);
        var img = frame.children('img');
        img.hide();
        var imgtemp = $(new Image());
        imgtemp.load(function () {
            var imgW = this.width;
            var imgH = this.height;
            var frameW = frame.width();
            var frameH = frame.height();
            var wScale = imgW / frameW;
            var hScale = imgH / frameH;
            var scale = wScale > hScale ? hScale : wScale;
            imgW = imgW / scale;
            imgH = imgH / scale;
            img.width(imgW);
            img.height(imgH);
            if (wScale > hScale) {
                var difW = (imgW - frameW) / 2;
                difW = (difW > 0) ? difW : 0;
                img.css('marginLeft', -difW);
            } else {
                var difT = (imgH - frameH) / 2;
                difT = (difT > 0) ? difT : 0;
                img.css('marginTop', -difT);
            }
            img.show();
        });
        imgtemp.error(function () {
            if (img.attr('noerror') == '1') {
                return true;
            }
            img.show();
        });
        imgtemp.attr('src', img.attr("src"));
    });
};
//--------------------图片自适应   end---------------------------


//---------------------默认Ajax状态处理器js   start---------------------------------------
var statusRouterHandler = {
    //消息处理器
    infoAction: function (jqXhr, data) {
        $.msgInfo(data.message);
    },
    //警告处理器
    causionAction: function (jqXhr, data) {
        $.msgWarn(data.message);
    },
    //错误处理器
    errorAction: function (jqXhr, data) {
        $.msgError(data.message);
    },
    //严重错误处理器
    seriousErrorAction: function (jqXhr, data) {
        $.msgAlert(data.message);
    },
    //身份过期处理器
    loginAction: function (jqXhr, data) {
        var message = '身份已过期，请重新登录！';
        if (data && data.message) {
            message = data.message;
        }
        $.msgError(message);
        setTimeout(function () {
            window.location = window.location;
        }, 1000);
    },
    //跳转处理器
    redirectAction: function (jqXhr, data) {
        var message = '系统跳转中';
        if (data && data.message) {
            message = data.message;
        }
        $.msgSuccess(message);
        setTimeout(function () {
            window.location.href = data.redirectUrl;
        }, 1000);
    },
    //未实现
    notImplementedAction: function (jqXhr, data) {
        var message = '未实现！';
        if (data && data.message) {
            message = data.message;
        }
        $.msgError(message);
    },
    //未找到此方法
    notFoundAction: function (jqXhr, data) {
        var message = '未找到此方法！';
        if (data && data.message) {
            message = data.message;
        }
        $.msgError(message);
    },
    //不被允许，没有权限
    forbiddenAction: function (jqXhr, data) {
        var message = '没有权限！';
        if (data && data.message) {
            message = data.message;
        }
        $.msgError(message);
    },
    //请求错误
    requestErrorAction: function (jqXhr, data) {
        var message = '请求异常！';
        if (data && data.message) {
            message = data.message;
        }
        $.msgError(message);
    }
};

//默认状态路由器
var defaultStatusRouter = new ManagedStatusRouter(statusRouterHandler);
//---------------------Ajax状态处理器js  end---------------------------------------


//禁止鼠标拖动，如拖动图片、连接等
document.ondragstart = function (event) {
    return false;
};