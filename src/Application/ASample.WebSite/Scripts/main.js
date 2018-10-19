/*!
 * main.js中的所有脚本只适用于人力资源，不适合共用
 * 
 *
 * Copyright 2017 fangzhongteng
 */

//---------------------------------------------------验证控件
//验证下拉--------------------------------
$.fn.validSelect = function () {
    var oThis = $(this);
    var isValide = true;
    oThis.parent().find('.ui-select').each(function (i) {
        var currentSelect = $(this);
        var requiredErrorCanvas = $(".field-validation-error[data-valmsg-for='" + currentSelect.attr('id') + "']");
        requiredErrorCanvas.hide();
        if (currentSelect.attr('data-val') === "true" && currentSelect.val() === '') {
            var errrorRequired = currentSelect.attr('data-val-required');
            requiredErrorCanvas.html(errrorRequired).show();
            isValide = false;
        }
    });
    return isValide;
};

//验证图片
$.fn.validImage = function () {
    var oThis = $(this);
    var isValide = true;
    oThis.parent().find('.img-hidden').each(function (i) {
        var currentHidden = $(this);
        var requiredErrorCanvas = $(".field-validation-error[data-valmsg-for='" + currentHidden.attr('id') + "']");
        requiredErrorCanvas.hide();
        if (currentHidden.attr('data-val') === "true" && currentHidden.val() === '') {
            var errrorRequired = currentHidden.attr('data-val-required');
            requiredErrorCanvas.html(errrorRequired).show();
            isValide = false;
        }
    });
    return isValide;
};

//---------------------------------------------------
//上传图片错误
$.extend({
    uploadError: function (errorType) {
        var errorMsg = '上传文件不符合要求！';
        switch (errorType) {
            case 'Q_EXCEED_NUM_LIMIT ':
                var fileNum = this.option('fileNumLimit ');
                errorMsg = '上传文件不能超过'+fileNum+'个！';
                break;
            case 'Q_EXCEED_SIZE_LIMIT':
               var fileSize= this.option('fileSizeLimit')/(1024*1024);
                errorMsg = '文件不能超过'+fileSize+'M！';
                break;
            case 'Q_TYPE_DENIED':
                var acceptObject = this.option('accept');
                if (acceptObject && acceptObject.extensions) {
                    errorMsg = '只允许上传' + acceptObject.extensions + '的文件！';
                } else {
                    errorMsg = '上传文件的格式不符合要求！';
                }
                break;
        }
        $.msgError(errorMsg);
    }
});

//发送验证码倒计时
$.fn.countDown=function() {
    var oThis = $(this);
    clearInterval(oThis.attr('codeTimeIndex'));
    oThis.html('<em class="num">59</em>秒后重发</a>');
    oThis.addClass('code-disable');
    oThis.attr('time', 59);
    var codeTimeIndex= setInterval(function () {
        var time = parseInt(oThis.attr('time'));
        time = time - 1;
        if (time > 0) {
            oThis.html('<em class="num">' + time + '</em>秒后重发</a>');
            oThis.attr('time', time);
            return false;
        }
        oThis.removeClass('code-disable');
        oThis.html('重新发送');
        clearInterval(oThis.attr('codeTimeIndex'));
        return false;
    }, 1000);
    oThis.attr('codeTimeIndex', codeTimeIndex);
};

//未完成功能提示
function nofeatures() {
    $.msgInfo('功能正在开发中，敬请期待！');
};




