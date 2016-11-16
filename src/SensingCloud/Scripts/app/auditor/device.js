$(function () {
    'use strict';
    var UrlEnabledDevices = "/DeviceManagement/GetAuditingEnabledDevices";
    var UrlDisabledDevices = "/DeviceManagement/GetAuditingDisabledDevices";
    var UrlEnableApproved = "/DeviceManagement/OnlineApproved";
    var UrlEnableRejected = "/DeviceManagement/OnlineRejected";
    var UrlDisableApproved = "/DeviceManagement/OfflineApproved";
    var UrlDisableRejected = "/DeviceManagement/OfflineRejected";
    var UrlGetDeviceDetail = "/DeviceManagement/GetDeviceDetail";

    var tab_1 = '审核上线设备';
    var tab_2 = '审核下线设备';
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
    //同意上线
    $('#tab_1_content').on('click', 'a[data-enable-approve]', function (e) {
        var device_id = $(this).attr('data-enable-approve');
        var options = {
            method: 'post',
            data: { ids: [device_id] }
        };
        $.ajax(UrlEnableApproved, options).success(function () {
            var options = {
                method: 'get',
                data: { query: $('input[name=query]').val() }
            };
            $.ajax(UrlEnabledDevices, options).success(function (data) {
                $('#tab_1_content').html(data);
            });
        });
    });
    //拒绝上线
    $('#tab_1_content').on('click', 'a[data-enable-reject]', function (e) {
        var device_id = $(this).attr('data-enable-reject');
        var options = {
            method: 'post',
            data: { id: device_id }
        };
        $.ajax(UrlEnableRejected, options).success(function () {
            var options = {
                method: 'get',
                data: { query: $('input[name=query]').val() }
            };
            $.ajax(UrlEnabledDevices, options).success(function (data) {
                $('#tab_1_content').html(data);
            });
        });
    });
    
    //全选审核上线设备
    $('#tab1').on('click', 'th input:checkbox', function (e) {
        if ($(this).prop('checked')) {
            $('table tr input:checkbox').prop('checked', true);
        } else {
            $('table tr input:checkbox').prop('checked', false);
        }
    });

    //一键上线
    $('#tab1').on('click', 'button[data-action-oneclick]', function (e) {
        console.log("一键上线");
        var ids = [];
        $('#tab1').find('table tbody tr input:checked').each(function () {
            var id = $(this).closest('tr').attr('id');
            ids.push(id);
        });
        var options = {
            method: 'post',
            data: { ids: ids }
        };
        $.ajax(UrlEnableApproved, options).success(function () {
            var options = {
                method: 'get',
                data: { query: $('input[name=query]').val() }
            };
            $.ajax(UrlEnabledDevices, options).success(function (data) {
                $('#tab_1_content').html(data);
            });
        });
    });

    /**************************审核下线设备************************/
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
    //同意下线
    $('#tab_2_content').on('click', 'a[data-disable-approve]', function (e) {
        var device_id = $(this).attr('data-disable-approve');
        var options = {
            method: 'post',
            data: { ids: [device_id] }
        };
        $.ajax(UrlDisableApproved, options).success(function () {
            var options = {
                method: 'get',
                data: { query: $('input[name=query]').val() }
            };
            $.ajax(UrlDisabledDevices, options).success(function (data) {
                $('#tab_2_content').html(data);
            });
        });
    });

    //拒绝下线
    $('#tab_2_content').on('click', 'a[data-disable-reject]', function (e) {
        var device_id = $(this).attr('data-disable-reject');

        var options = {
            method: 'post',
            data: { id: device_id }
        };
        $.ajax(UrlDisableRejected, options).success(function () {
            var options = {
                method: 'get',
                data: { query: $('input[name=query]').val() }
            };
            $.ajax(UrlDisabledDevices, options).success(function (data) {
                $('#tab_2_content').html(data);
            });
        });
    });
    //全选审核下线设备
    $('#tab2').on('click', 'th input:checkbox', function (e) {
        if ($(this).prop('checked')) {
            $('table tr input:checkbox').prop('checked', true);
        } else {
            $('table tr input:checkbox').prop('checked', false);
        }
    });

    //一键下线
    $('#tab2').on('click', 'button[data-action-oneclick]', function (e) {
        console.log("一键下线");
        var ids = [];
        $('#tab2').find('table tbody tr input:checked').each(function () {
            var id = $(this).closest('tr').attr('id');
            ids.push(id);
        });
        var options = {
            method: 'post',
            data: { ids: ids }
        };
        $.ajax(UrlDisableApproved, options).success(function () {
            var options = {
                method: 'get',
                data: { query: $('input[name=query]').val() }
            };
            $.ajax(UrlDisabledDevices, options).success(function (data) {
                $('#tab_2_content').html(data);
            });
        });
    });
   
    //***************详细****************

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

    $('#tab_2_content').on('click', 'a[data-disable-info]', function (e) {
        e.preventDefault();
        var device_id = $(this).attr('data-disable-info');
        naviateToDetail(device_id);
        return false;
       
    });
    $('#tab_1_content').on('click', 'a[data-enable-info]', function (e) {
        e.preventDefault();
        var device_id = $(this).attr('data-enable-info');

        naviateToDetail(device_id);
        return false;

    });

    //*****************返回******************
    $("#device-detail").on('click', '#returnToList', function (e) { 
        $('#devices-page').show();
        $('#device-detail').hide();
    });


});

