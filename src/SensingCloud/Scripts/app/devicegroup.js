$(function () {
    'use strict';
    var app = $.sammy(function () {
        var $list_section = $('#list-section');
        var $ajax_section = $('#ajax-section');

        //回到主页面
        this.get('#/', function () {
            $.get('/DeviceManagement/DeviceGroupList', {query:$('input[name=query]').val()}, function (data) {
                $ajax_section.html('').hide();
                $('#table-container').html(data);
                $list_section.show();
            });
            return false;
        });
        this.get('#/new_devicegroup', function () {
            $.get('/DeviceManagement/createDeviceGroup', function (data) {
                $list_section.hide();
                $ajax_section.html(data).show();
                $.validator.unobtrusive.parse("form");
            });
            return false;
        });
        this.get('#/edit_devicegroup/:id', function (context) {
            var user_id = context.params['id'];
            $.get('/devicemanagement/editDeviceGroup', { id: user_id }).success(function (data) {
                $ajax_section.html(data).show();
                $('#editDeviceGroupDialog').modal('show');
                $('#editDeviceGroupDialog').on('hidden.bs.modal', function (e) {
                    context.redirect("#/");
                });
                $('button[type=submit]').click(function () {
                    $ajax_section.find('form').submit();
                });
            });
            return false;
        });
        this.get('#/delete_devicegroup/:id', function (context) {
            var user_id = context.params['id'];
            if (confirm("确定删除该组吗？")) {
                var options = {
                    method: 'delete',
                    data: { id: user_id }
                };
                $.ajax('/devicemanagement/DeleteDeivceGroup', options).success(function (data) {
                    context.redirect("#/");
                });
            }
            else {
                context.redirect("#/");
            }
            return false;
        });


    });
    app.run();

});

