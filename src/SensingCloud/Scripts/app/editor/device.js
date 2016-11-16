$(function () {
    'use strict';
    ///editor urls
    var UrlEnabledDevices = "/DeviceManagement/GetEnabledDevices";
    var UrlDisabledDevices = "/DeviceManagement/GetDisabledDevices";
    var UrlAuditingDevices = "/DeviceManagement/GetAuditingDevices";

    var UrlEnableDeviceAction = "/DeviceManagement/EnableDevice";
    var UrlDisableDeviceAction = "/DeviceManagement/DisableDevice";
    var UrlCancelDeviceAction = "/DeviceManagement/CancelDevice";
    var UrlGetDeviceDetail = "/DeviceManagement/GetDeviceDetail";
    var UrlDeleteDevice = "/DeviceManagement/DeleteDevice";
    var tab_1 = '上线设备';
    var tab_2 = '下线设备';
    var tab_3 = '待审核设备';
    var currentTab = tab_1;

    (function () {
        var options = {
            method: 'get',
            data: { query: $('input[name=query]').val() }
        };
        $.ajax(UrlEnabledDevices, options).success(function (data) {
            $('#tab_1_content').html(data);
        });
    })();

    //switch button
    $('input[name=switcher]').switchButton({
        onLabel: '列表',
        offLabel: '表格',
        fontSize: '12px',
        width: 70,
        height: 33,
    });

    $('a[href=#tab1]').click(function () {
        var options = {
            method: 'get',
            data: { query: $('input[name=query]').val() }
        };
        $.ajax(UrlEnabledDevices, options).success(function (data) {
            $('#tab_1_content').html(data);
            currentTab = tab_1;
        });
    });
    //使用ajax请求分页
    $('#tab_1_content').on('click', '.row-pager a', function (e) {
        e.preventDefault();
        var url = $(e.target).attr('href');
        if (url) {
            var options = {
                method: 'get',

            };
            $.ajax(url, options).success(function (data) {
                $('#tab_1_content').html(data);
            });
        }
        return false;
    });
    //查询设备
    $('.search-form').submit(function (e) {
        e.preventDefault();
        if (currentTab != tab_1) return false;
        var options = {
            method:'get',
            url: UrlEnabledDevices,
            data:$(this).serialize()
        };
        
        $.ajax(options).success(function (data) {
            $('#tab_1_content').html(data);
        });
        return false;
    });
    //上线设备列表和表格切换
    $('.switch_1 input[type=checkbox]').change(function () {
        var options = {
            method: 'get',
            data: { query: $('input[name=query]').val() }
        };
        if ($(this).is(':checked')) {
            options.data.mode = 'grid';
        } else {
            options.data.mode = 'list';
        }
        $.ajax(UrlEnabledDevices, options).success(function (data) {
            $('#tab_1_content').html(data);
        });
    });
    //用户申请上线设备
    $('#tab_1_content').on('click', 'a[data-action-disable]', function (e) {
        e.preventDefault();
        var device_id = $(this).attr('data-action-disable');
        $.post(UrlDisableDeviceAction, { ids: [device_id] }).success(function () {
            //reload
            var options = {
                method: 'get',
                data: { query: $('input[name=query]').val() }
            };
            $.ajax(UrlEnabledDevices, options).success(function (data) {
                $('#tab_1_content').html(data);
            });
        });
        return false;
    });

    //全选上线设备
    $('#tab1').on('click', '#checkAllOnlineDevice', function (e) {
        if (this.checked) {
            $("tr input:checkbox").each(function () {
                this.checked = true;
            });
        } else {
            $("tr input:checkbox").each(function () {
                this.checked = false;
            });
        }
    });

    //一键下线选中的设备
    $('#tab1').on('click', 'th input:checkbox', function (e) {
        if ($(this).prop('checked')) {
            $('table tr input:checkbox').prop('checked', true);
        } else {
            $('table tr input:checkbox').prop('checked', false);
        }
    });

    //一键申请下线
    $('#tab1').on('click', 'button[data-action-oneclick]', function (e) {
        console.log("一键下线");
        var ids = [];
        $('#tab1').find('table tbody tr input:checked').each(function () {
            var id = $(this).closest('tr').attr('id');
            ids.push(id);
        });
        var options = {
            method: 'post',
            data: { ids: ids }
        };
        $.ajax(UrlDisableDeviceAction, options).success(function () {
            var options = {
                method: 'get',
                data: { query: $('input[name=query]').val() }
            };
            $.ajax(UrlEnabledDevices, options).success(function (data) {
                $('#tab_1_content').html(data);
            });
        });
    });


    /**************************下线设备************************/
    $('a[href=#tab2]').click(function () {
        var options = {
            method: 'get',
            data: { query: $('input[name=query]').val() }
        };
        $.ajax(UrlDisabledDevices, options).success(function (data) {
            $('#tab_2_content').html(data);
            currentTab = tab_2;
        });
    });
    $('#tab_2_content').on('click', '.row-pager a', function (e) {
        e.preventDefault();
        var url = $(e.target).attr('href');
        if (url) {
            var options = {
                method: 'get',
            };
            $.ajax(url, options).success(function (data) {
                $('#tab_2_content').html(data);
            });
        }
        return false;
    });
    //查询设备
    $('.search-form').submit(function (e) {
        e.preventDefault();
        if(currentTab != tab_2) return false;
        var options = {
            method: 'get',
            url: UrlDisabledDevices,
            data: $(this).serialize()
        };

        $.ajax(options).success(function (data) {
            $('#tab_2_content').html(data);
        });
        return false;
    });
    //上线设备列表和表格切换
    $('.switch_2 input[type=checkbox]').change(function () {
        var options = {
            method: 'get',
            data: { query: $('input[name=query]').val() }
        };
        if ($(this).is(':checked')) {
            options.data.mode = 'grid';
        } else {
            options.data.mode = 'list';
        }
        $.ajax(UrlDisabledDevices, options).success(function (data) {
            $('#tab_2_content').html(data);
        });
    });
    //用户申请下线设备
    $('#tab_2_content').on('click', 'a[data-action-enable]', function () {
        var device_id = $(this).attr('data-action-enable');
        $.post(UrlEnableDeviceAction, { ids: [device_id] }).success(function () {
            //reload
            var options = {
                method: 'get',
                data: { query: $('input[name=query]').val() }
            };
            $.ajax(UrlDisabledDevices, options).success(function (data) {
                $('#tab_2_content').html(data);
            });
        });
    });
    $('#tab_2_content').on('click', 'a[data-action-delete]', function () {
        var device_id = $(this).attr('data-action-delete');
        $.post(UrlDeleteDevice, { id: device_id }).success(function () {
            //reload
            var options = {
                method: 'get',
                data: { query: $('input[name=query]').val() }
            };
            $.ajax(UrlDisabledDevices, options).success(function (data) {
                $('#tab_2_content').html(data);
            });
        });
    });


    //一键上线选中的设备
    $('#tab2').on('click', 'th input:checkbox', function (e) {
        if ($(this).prop('checked')) {
            $('table tr input:checkbox').prop('checked', true);
        } else {
            $('table tr input:checkbox').prop('checked', false);
        }
    });

    //一键申请上线
    $('#tab2').on('click', 'button[data-action-oneclick]', function (e) {
        console.log("一键申请上线");
        var ids = [];
        $('#tab2').find('table tbody tr input:checked').each(function () {
            var id = $(this).closest('tr').attr('id');
            ids.push(id);
        });
        var options = {
            method: 'post',
            data: { ids: ids }
        };
        $.ajax(UrlEnableDeviceAction, options).success(function () {
            var options = {
                method: 'get',
                data: { query: $('input[name=query]').val() }
            };
            $.ajax(UrlDisabledDevices, options).success(function (data) {
                $('#tab_2_content').html(data);
            });
        });
    });

    /*************************待审核的设备******************************/
    $('a[href=#tab3]').click(function () {
        var options = {
            method: 'get',
            data: { query: $('input[name=query]').val() }
        };
        $.ajax(UrlAuditingDevices, options).success(function (data) {
            $('#tab_3_content').html(data);
            currentTab = tab_3;
        });
    });
    $('#tab_3_content').on('click', $('.row-pager ul li a'), function (e) {
        e.preventDefault();
        var url = $(e.target).attr('href');
        if (url) {
            var options = {
                method: 'get',
            };
            $.ajax(url, options).success(function (data) {
                $('#tab_3_content').html(data);
            });
        }
        return false;
    });
    //查询设备
    $('.search-form').submit(function (e) {
        e.preventDefault();
        if (currentTab != tab_3) return false;
        var options = {
            method: 'get',
            url: UrlAuditingDevices,
            data: $(this).serialize()
        };

        $.ajax(options).success(function (data) {
            $('#tab_3_content').html(data);
        });
        return false;
    });
    //上线设备列表和表格切换
    $('.switch_3 input[type=checkbox]').change(function () {
        var options = {
            method: 'get',
            data: { query: $('input[name=query]').val() }
        };
        if ($(this).is(':checked')) {
            options.data.mode = 'grid';
        } else {
            options.data.mode = 'list';
        }
        $.ajax(UrlAuditingDevices, options).success(function (data) {
            $('#tab_3_content').html(data);
        });
    });
    //取消用户设备申请
    $('#tab_3_content').on('click', 'a[data-action-cancel]', function (e) {
        e.preventDefault();
        var device_id = $(this).attr('data-action-cancel');
        $.post(UrlCancelDeviceAction, { ids: [device_id] }).success(function () {
            //reload
            var options = {
                method: 'get',
                data: { query: $('input[name=query]').val() }
            };
            $.ajax(UrlAuditingDevices, options).success(function (data) {
                $('#tab_3_content').html(data);
            });
        });
        return false;
    });

   //一键取消审清
    $('#tab3').on('click', 'th input:checkbox', function (e) {
        if ($(this).prop('checked')) {
            $('table tr input:checkbox').prop('checked', true);
            } else {
            $('table tr input:checkbox').prop('checked', false);
            }
        });

   //一键申请上线
    $('#tab3').on('click', 'button[data-action-oneclick]', function (e) {
        console.log("一键取消申请");
        var ids = [];
        $('#tab3').find('table tbody tr input:checked').each(function () {
            var id = $(this).closest('tr').attr('id');
            ids.push(id);
        });
        var options = {
                method: 'post',
                    data: { ids: ids }
        };
        $.ajax(UrlCancelDeviceAction, options).success(function () {
                var options = {
                    method: 'get',
                    data: { query: $('input[name=query]').val() }
                    };
                $.ajax(UrlAuditingDevices, options).success(function (data) {
                    $('#tab_3_content').html(data);
                    });
                });
                });

    //************设备详细*************
    function naviateToDetail(device_id) {
        var options = {
            method: 'get',
            data: { id: device_id }
        };
        $.ajax(UrlGetDeviceDetail, options).success(function (data) {
            $('#devices-page').hide();
            $('#device-detail').html(data).show();
        });
    }

    $('#tab1').on('click', 'a[data-enabled-info]', function (e) {
        e.preventDefault();
        var device_id = $(this).attr('data-enabled-info');

        naviateToDetail(device_id);
        return false;

    });

    $('#tab2').on('click', 'a[data-disabled-info]', function (e) {
        e.preventDefault();
        var device_id = $(this).attr('data-disabled-info');
        naviateToDetail(device_id);
        return false;
    });

    $('#tab_3_content').on('click', 'a[data-auditing-info]', function (e) {
        e.preventDefault();

        var device_id = $(this).attr('data-auditing-info');
        naviateToDetail(device_id);
        return false;
    });

    //*****************返回******************
    $("#device-detail").on('click', '#returnToList', function (e) {
        $('#devices-page').show();
        $('#device-detail').hide();
    });
});

