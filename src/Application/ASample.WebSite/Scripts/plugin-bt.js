//baitu控件配置
var baituPluginOptions = {
    //获取grid的默认配置
    AjaxGridOption: function (columns, url, isMultiple, height) {
        //数据源
        var dataSource = {
            //数据概要
            schema: {
                data: function (data) {
                    return data.rows;
                },
                total: function (data) {
                    return data.total;
                },
                model: {
                    id: 'id'
                }
            },
            //数据运送配置
            transport: {
                read: function (options) {
                    //将filter转化为正常的请求参数
                    if (options.data['filter'] && options.data['filter'].filters.length > 0) {
                        $.each(options.data['filter'].filters, function (i) {
                            var filter = options.data['filter'].filters[i];
                            var name = filter['field'];
                            var value = filter['value'];
                            options.data[name] = value;
                        });
                    }

                    var successFn = function (result) {
                        options.success(result);
                    };
                    $.ajaxPost(url, options.data, successFn,'',null,null,true);
                }
            },
            page: 1,
            pageSize: 10,
            serverFiltering:true,
            serverPaging: true
        };

        //分页配置
        var pageOption = {
            //20171106 dq  按钮太多后面的文字会被挤到第二行
            buttonCount: 5,  
            input: true
        };

        var gridOption = {
            //列配置
            columns: columns,
            //数据源
            dataSource: dataSource,
            //分页配置
            pageable: pageOption,
            resizable:true
        }
        //多选就不用指定选择模式
        if (!isMultiple) {
            gridOption.selectable = 'row';
        }

        //指定了高度
        if (height) {
            gridOption['height'] = height;
            gridOption['scrollable'] = true;
        }
        return gridOption;
    },

    //获取treeList的默认配置
    AjaxTreeListOption: function (columns, url, height) {
        //数据源
        var dataSource = {
            //数据概要
            schema: {
                model: {
                    id: 'id',
                    parentId: 'parentId',
                    //加载多层tree
                    fields: {
                        //nullable为false:当字段为空是，不会转为default value
                        parentId: {
                            type: 'string', parse: function (dataItem) {
                                if (dataItem) {
                                    return dataItem;
                                }
                                return '';
                            }
                        },
                        id: { type: 'string' }
                    },
                    expanded: false
                }
            },
            //数据运送配置
            transport: {
                read: function (options) {
                    var successFn = function (result) {
                        options.success(result);
                    };
                    $.ajaxPost(url, options.data, successFn);
                }
            }
        };

        var treeListOption = {
            //列配置
            columns: columns,
            //数据源
            dataSource: dataSource,
            selectable: "row",
            //中文提示  dq 20171011
            messages: {
                noRows: "没有可显示的记录"
            }
        }
        //指定了高度
        if (height) {
            treeListOption['height'] = height;
            treeListOption['sortable'] = false;
        }
        return treeListOption;
    },

    //获取multiselect的默认配置
    MultiselectOption: function (data, valueField, textField) {
        var option = {
            animation: false,
            autoClose: false,
            clearButton: false,
            //搜索采用包含 
            filter: 'contains',
            placeholder: '请选择',
            noDataTemplate: '没有数据',
            //不区分大小写
            ignoreCase: false,
            //输入1个字开始搜索
            minLength: 1
        };
        //赋值
        if (data) {
            option['dataSource'] = { data: data }
        }
        //设置显示和值字段
        if (textField) {
            option['dataTextField'] = textField;
        }
        if (valueField) {
            option['dataValueField'] = valueField;
        }
        return option;
    },

    //获取comboBox的默认配置
    ComboBoxOption: function (data, valueField, textField) {
        var option = {
            animation: false,
            placeholder: '请选择',
            noDataTemplate: '没有数据',
            //过滤规则
            filter: 'contains'
        };
        if (data) {
            option['dataSource'] = { data: data }
        }
        //设置显示和值字段
        if (textField) {
            option['dataTextField'] = textField;
        }
        if (valueField) {
            option['dataValueField'] = valueField;
        }
        return option;
    },

    //获取DatePicker配置
    DatePickerOption: function () {
        var option = {
            animation: false,
            culture: 'zh-CN',
            format: "yyyy-MM-dd"
        };
        return option;
    },

    //获取DateTimePicker
    DateTimePickerOption: function () {
        var option = {
            animation: false,
            culture: 'zh-CN',
            format: "yyyy-MM-dd"
        };
        return option;
    }
};

//日历开始时间与结束时间联动方法
var dataPickerFn = {
    //绑定联动事件
    bindLinkChange: function (start, end) {
        if (!start || !end) return;
        //开始时间控件绑定change事件
        start.bind("change", function () {
            var startDate = start.value();
            var endDate = end.value();
            if (startDate) {
                startDate = new Date(startDate);
                startDate.setDate(startDate.getDate());
                end.min(startDate);
            } else if (endDate) {
                start.max(new Date(endDate));
            } else {
                endDate = new Date();
                start.max(endDate);
                end.min(endDate);
            }
        });
        //结束时间控件绑定change事件
        end.bind("change", function () {
            var endDate = end.value();
            var startDate = start.value();
            if (endDate) {
                endDate = new Date(endDate);
                endDate.setDate(endDate.getDate());
                start.max(endDate);
            } else if (startDate) {
                end.min(new Date(startDate));
            } else {
                endDate = new Date();
                start.max(endDate);
                end.min(endDate);
            }
        });
        start.max(end.value());
        end.min(start.value());
    }
}

//grid的拓展方法------------------------------------------------
//20171031  dq  新增参数notWarn
$.fn.baituGridGetSelections = function (isSingle, notWarn) {
    var grid = $(this).data('kendoGrid');
    var selectData = [];
    //选中的行
    var selectRows = grid.select();
    if (selectRows.length === 0 && !notWarn) {
        $.msgWarn('请选择要操作的数据！');
    }
    else if (isSingle && selectRows.length > 1) {
        $.msgWarn('只能选择一条数据操作！');
    }
    $.each(selectRows, function (i) {
        var dataRow = grid.dataItem($(this));
        selectData.push(dataRow);
    });
    return selectData;
};

//刷新(当前页)
$.fn.baituGridRefresh = function () {
    $(this).data('kendoGrid').dataSource.query();
};

//刷新（回到第一页）
$.fn.baituGridLoad = function (data) {
    data = data || {}
    var filters = [];
    $.each(data, function (key) {
        var value = data[key];
        filters.push({ field: key, value: value });
    });
    $(this).data('kendoGrid').dataSource.filter(filters);
};

//选中第一行
$.fn.baituGridSelectFirst = function () {
    var grid = $(this).data('kendoGrid');
    var dataSouce = grid.dataSource;
    var currentRow = dataSouce.at(0);
    if (currentRow) {
        var filter = 'tr[data-uid="' + currentRow['uid'] + '"]';
        grid.select(filter);
    }
}

//选中指定行
$.fn.baituGridSelect = function (ids) {
    var grid = $(this).data('kendoGrid');
    var dataSouce = grid.dataSource;
    //先清除所有选中行   dq 20171011
    grid.clearSelection();
    if (ids && ids.length > 0) {
        $.each(ids, function (i) {
            var currentRow = dataSouce.get(this);
            var filter = 'tr[data-uid="' + currentRow['uid'] + '"]';
            grid.select(filter);
        });
    }
};

//获取高度----------------------------------------------------------
$.extend({
    //获取主的高度
    getMainCanvasHeight: function (btnTools, searchCanvas) {
        var mainHeight = $(window).height() - 110-41-25;
        //工具栏
        if (btnTools&&btnTools.length>0) {
            mainHeight = mainHeight - btnTools.height();
        }
        //搜索栏
        if (searchCanvas && searchCanvas.length > 0) {
            mainHeight = mainHeight - searchCanvas.height();
        }
        
        return mainHeight;
    }
});

//treeList的拓展方法------------------------------------------------

//获取TreeList选中
//20171027  dq  新增notWarn参数  
$.fn.baituTreeListGetSelections = function (isSingle, notWarn) {
    var treeList = $(this).data('kendoTreeList');
    var selectDatas = [];
    //选中的行
    var selectRows = treeList.select();
    if (selectRows.length === 0 && !notWarn) {
        $.msgWarn('请选择要操作的数据！');
    }
    else if (isSingle && selectRows.length > 1) {
        $.msgWarn('只能选择一条数据操作！');
    }
    $.each(selectRows, function (i) {
        var dataRow = treeList.dataItem($(this));
        selectDatas.push(dataRow);
    });
    return selectDatas;
};

//获取TreeList的check
$.fn.baituTreeListGetCheck = function (filter) {
    var treeList = $(this).data('kendoTreeList');
    var allChecks = $(this).find(filter);
    var checkRows = [];
    allChecks.each(function () {
        if ($(this).prop('checked')) {
            var id = $(this).attr('id');
            var checkRow = treeList.dataSource.get(id);
            checkRows.push(checkRow);
        }
    });
    return checkRows;
};

//TreeList 创建check
$.fn.baituTreeListCheckColumn = function (filter) {
    var treeList = $(this).data('kendoTreeList');
    //选中父节点
    var checkParent = function (parentId) {
        var parentRow = treeList.dataSource.get(parentId);
        var parentFilter = '#' + parentId;
        //非选中才选父级
        if (!$(parentFilter).prop('checked')) {
            //触发父级选中时，不触发子集选中
            parentRow['isMethodCheck'] = true;
            $(parentFilter).iCheck('check');
            if (parentRow['parentId']) {
                checkParent(parentRow['parentId']);
            }
        }
    };

    //选中子节点
    var checkChild = function (id) {
        var currentRow = treeList.dataSource.get(id);
        if (currentRow['isMethodCheck']) {
            delete currentRow["isMethodCheck"];
            return false;
        }
        var data = treeList.dataSource.data();
        $.each(data, function (i) {
            var currentItem = this;
            //当前节点为子节点
            if (currentItem['parentId'] == id) {
                var filter = '#' + currentItem['id'];
                if (!$(filter).prop('checked')) {
                    $(filter).iCheck('check');
                    checkChild(currentItem['id']);
                }
            }
        });
    };

    //不选中父节点
    var uncheckParent = function (parentId) {
        var data = treeList.dataSource.data();
        for (var i = 0; i < data.length; i++) {
            var currentItem = data[i];
            var currentFilter = '#' + currentItem['id'];
            if (currentItem['parentId'] == parentId && $(currentFilter).prop('checked')) {
                return true;
            }
        }
        $('#' + parentId).iCheck('uncheck');
    };


    //不选中子节点
    var uncheckChild = function (id) {
        var data = treeList.dataSource.data();
        $.each(data, function (i) {
            var currentItem = this;
            //当前节点为子节点
            if (currentItem['parentId'] == id) {
                var filter = '#' + currentItem['id'];
                if ($(filter).prop('checked')) {
                    $(filter).iCheck('uncheck');
                    uncheckChild(currentItem['id']);
                }
            }
        });
    };

    //选中
    var treeListChecked = function (event) {
        var currentRow = $(this).closest('tr');
        var currentData = treeList.dataItem(currentRow);
        //选中父节点
        if (currentData['parentId']) {
            checkParent(currentData['parentId']);
        }
        //选中子节点
        var id = currentData['id'];
        checkChild(id);
    };

    //取消选中
    var treeListUnChecked = function (event) {
        var currentRow = $(this).closest('tr');
        var currentData = treeList.dataItem(currentRow);
        if (currentData['parentId']) {
            uncheckParent(currentData['parentId']);
        }
        //取消选中所有子节点
        var id = currentData['id'];
        uncheckChild(id);
    };

    var oThis = $(this);
    oThis.find(filter).iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green'
    });
    //选中事件
    oThis.find(filter).on('ifChecked', treeListChecked);
    //取消选中
    oThis.find(filter).on('ifUnchecked', treeListUnChecked);
};

//刷新
$.fn.baituTreeListRefresh = function (data) {
    data = data || {};
    $(this).data('kendoTreeList').dataSource.read(data);
}

//选中指定行
$.fn.baituTreeListChecked = function (ids, filter) {
    var treeList = $(this).data('kendoTreeList');
    var oThis = $(this);
    //取消所有选择
    oThis.find(filter).iCheck('uncheck');
    if (ids && ids.length > 0) {
        $.each(ids, function (i) {
            var currentId = ids[i];
            var currentRow = treeList.dataSource.get(currentId);
            //触发父级选中时，不触发子集选中
            currentRow['isMethodCheck'] = true;
            oThis.find('#' + currentId).iCheck('check');
        });
    }
};

//清空check
$.fn.baituTreeListClearChecked = function(filter) {
    var oThis = $(this);
    //取消所有选择
    oThis.find(filter).iCheck('uncheck');
};

//treeView的拓展方法------------------------------------------------
//获取TreeView  选中
$.fn.baituTreeViewGetSelections = function (isSingle) {
    var treeView = $(this).data('kendoTreeView');
    var selectDatas = [];
    //选中的行
    var selectRows = treeView.select();
    if (isSingle && selectRows.length > 1) {
        $.msgWarn('只能选择一条数据操作！');
    }
    $.each(selectRows, function (i) {
        var dataRow = treeView.dataItem($(this));
        selectDatas.push(dataRow);
    });
    return selectDatas;
};

//消息提示信息------------------------------------------------
$.extend({
    //弹出成功消息
    msgSuccess: function (msg) {
        toastr.remove();
        toastr.options = {
            'closeButton': false,
            'debug': false,
            'newestOnTop': false,
            'progressBar': true,
            'positionClass': 'toast-top-right',
            'preventDuplicates': true,
            'onclick': null,
            'showDuration': '300',
            'hideDuration': '1000',
            'timeOut': '5000',
            'extendedTimeOut': '1000',
            'showEasing': 'swing',
            'hideEasing': 'linear',
            'showMethod': 'fadeIn',
            'hideMethod': 'fadeOut'
        }
        toastr.success(msg);
    },
    //警告
    msgWarn: function (msg) {
        toastr.remove();
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-top-center",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.warning(msg);
    },

    //错误
    msgError: function (msg) {
        toastr.remove();
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-top-center",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.error(msg);
    },

    //消息
    msgInfo: function (msg) {
        toastr.remove();
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.info(msg);
    },
    //loading
    loading: function (msg) {
        if (msg) {
            layer.msg(msg, {
                icon: 16,
                time: 0,
                shade: 0.01,
                scrollbar: false
            });
        } else {
            layer.load(0, {
                scrollbar: false
            });
        }
    },

    //alert需要用户确认的
    msgAlert: function (msg) {
        layer.open({
            type: 0,
            icon: 0,
            content: msg
        });
    },

    //关闭loading
    hideLoading: function () {
        layer.closeAll('loading');
        layer.closeAll('dialog');
    },

    //询问提示框
    confirm: function (msg, callback) {
        layer.confirm(msg, { icon: 3, title: '提示', scrollbar: false }, function (index) {
            callback(true);
            layer.close(index);
        });
    }
});


//验证控件-----------------------------------------------------------
//验证kendo的下拉列表
$.fn.validKendoSelect = function () {
    var oThis = $(this);
    var isValide = true;
    oThis.parent().find('.kendo-select-hidden').each(function (i) {
        var currentSelect = $(this);
        var requiredErrorCanvas = $(".with-errors[data-valmsg-for='" + currentSelect.attr('id') + "']");
        requiredErrorCanvas.hide();
        if (currentSelect.attr('data-val') === "true" && currentSelect.val() === '') {
            var errrorRequired = currentSelect.attr('data-val-required');
            requiredErrorCanvas.removeClass('field-validation-valid').addClass('field-validation-error');
            requiredErrorCanvas.html(errrorRequired).show();
            isValide = false;
        }
    });
    return isValide;
};

//清空验证
$.fn.clearValidError=function() {
    var oThis = $(this);
    oThis.find('.field-validation-error').removeClass('.field-validation-error').empty();
}