
$(function () {

    var app = $.sammy(function () {
        var $dialog_container = $("#dialog_container");
        var $list = $('#list');
        //回到主页面
        this.get('#/', function () {
            $.get('/user', function (data) {
                $dialog_container.html('');
                $list.find('table').replaceWith(data);
                $list.show();
            });
            $('#editUserDialog').modal('hide');
            $('#deleteUserDialog').modal('hide');
            return false;
        });


        //添加标签内容，弹框模式
        this.get('#/new_tagValue/:id', function (context) {
            var tag_id = context.params['id'];
            $.get('/user/CreateTagValue', { id: tag_id }).success(function (data) {
                $dialog_container.html(data);
                $.validator.unobtrusive.parse("form");

                $('#addTagValueDialog').modal('show');
                $('#addTagValueDialog').on('hidden.bs.modal', function (e) {
                    context.redirect("#/");
                });
                $('#confirm_update_user').click(function (e) {
                    e.preventDefault();
                    if (!$('#addTagValueForm').valid()) return false;
                    var options = {
                        url: '/user/CreateTagValue',
                        method: 'post',
                        data: $('#addTagValueForm').serialize()
                    };

                    $.ajax(options)
                        .success(function (data) {
                            $('.alert-success').fadeIn().delay(500).fadeOut(function () {
                                $('#addTagValueDialog').modal('hide');
                                context.redirect('#/');
                            });
                        })
                        .error(function (data) {

                        });
                });
            });
            return false;
        });


    });
    app.run();
});
