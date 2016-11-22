
$(function () {

    $(document).on('change', '.lock-user input[type=checkbox]', function () {
        var chk = $(this);
        var userId = chk.parents('tr').attr('id');
        var options = {
            method: 'post',
            data: { id: userId }
        };
        if (chk.is(':checked')) {
            options.url = "/user/lockuser";
        } else {
            options.url = "/user/unlockuser";
        }
        $.ajax(options).success(function () { console.log("状态改变成功") });

    });

    //使用ajax请求分页
    $('#userTable').on('click', '.row-pager a', function (e) {
        e.preventDefault();
        var url = $(e.target).attr('href');
        if (url) {
            var options = {
                method: 'get',

            };
            $.ajax(url, options).success(function (data) {
                $('#userTable').html(data);
            });
        }
        return false;
    });

    $('.search-form').submit(function (e) {
        e.preventDefault();
        var url = $(this).attr('action');
        var options = {
            method: 'get',
            url: url,
            data: $(this).serialize()
        };

        $.ajax(options).success(function (data) {
            $('#userTable').html(data);
        });
        return false;
    });

        var app = $.sammy(function () {
            var $dialog_container = $("#dialog_container");
            var $list = $('#list');
            //回到主页面
            this.get('#/', function () {
                $.get('/user', function (data) {
                    $dialog_container.html('');
                    $list.find('#userTable').html(data);
                    $list.show();
                });
                $('#editUserDialog').modal('hide');
                $('#deleteUserDialog').modal('hide');
                return false;
            });

            //新建用户
            this.get('#/new_user', function (context) {
                $.get('/user/newuser').success(function (data) {
                    $dialog_container.html(data);
                    $.validator.unobtrusive.parse("form");
                    $list.hide();
                    initialze();
                    function initialze() {
                        var $form = $('#createForm');
                        $('#file').pictureSelector();

                        //ajax submit
                        $form.submit(function (e) {
                            e.preventDefault();
                            $(this).ajaxSubmit({
                                success: function (data) {
                                    $dialog_container.html(data);
                                    initialze();
                                    $('.alert-success').fadeIn().delay(2000).fadeOut();
                                },
                                error: function (jqxhr, status, error) {
                                    console.log(error);
                                }
                            });
                            return false;
                        });

                        //when "reset" button of form is hit, file field will be reset, but the custom UI won't
                        //so you should reset the ui on your own
                        $form.on('reset', function () {
                            $(this).find('input[type=file]').ace_file_input('reset_input_ui');
                        });
                        if (location.protocol == 'file:') alert("For uploading to server, you should access this page using 'http' protocal, i.e. via a webserver.");
                    }

                   
                });
                return false;
            });

            //编辑用户
            this.get('#/edit_user/:id', function (context) {
                var user_id = context.params['id'];
                $.get('/user/edituser', { id: user_id }).success(function (data) {
                    $dialog_container.html(data);
                    $('#editUserDialog').modal('show');
                    $('#editUserDialog').on('hidden.bs.modal', function (e) {
                        context.redirect("#/");
                    });
                    $('#confirm_update_user').click(function () {
                        var options = {
                            url:'/user/updateuser',
                            method: 'put',
                            data: $('#editUserForm').serialize()
                        };

                        $.ajax(options)
                            .success(function (data) {
                                $('#' + user_id).replaceWith(data);
                                $('#editUserDialog').modal('hide');
                                context.redirect('#/');
                            })
                            .error(function (data) {

                            });
                    });
                });
                return false;
            });


            //删除用户
            this.get('#/delete_user/:id', function (context) {
                var user_id = context.params['id'];
                $.get('/user/deleteuser', { id: user_id }).success(function (data) {
                    $dialog_container.html(data);
                    $('#deleteUserDialog').modal('show');
                    $('#deleteUserDialog').on('hidden.bs.modal', function (e) {
                        context.redirect("#/");
                    });
                    $('#confirm_delete_user').click(function (e) {
                        var url = $(this).attr('href');
                        var options = {
                            url: url,
                            method: 'delete',
                        };
                        $.ajax(options)
                            .success(function () {
                                $('#deleteUserDialog').modal('hide');
                                $('#'+user_id).remove();
                            })
                            .done(function () {
                                context.redirect('#/');
                            });
                            
                        return false;
                    });
                });
            });

            //重置用户密码
            this.get('#/reset_user/:id', function (context) {
                var user_id = context.params['id'];
                $.get('/user/AdminResetPassword', { id: user_id }).success(function (data) {
                    $dialog_container.html(data);
                    $.validator.unobtrusive.parse("form");

                    $('#resetUserDialog').modal('show');
                    $('#resetUserDialog').on('hidden.bs.modal', function (e) {
                        context.redirect("#/");
                    });
                    $('#confirm_update_user').click(function (e) {
                        e.preventDefault();
                        if (!$('#resetUserForm').valid()) return false;
                        var options = {
                            url: '/user/AdminResetPassword',
                            method: 'post',
                            data: $('#resetUserForm').serialize()
                        };

                        $.ajax(options)
                            .success(function (data) {
                                $('.alert-success').fadeIn().delay(500).fadeOut(function () {
                                    $('#resetUserDialog').modal('hide');
                                    context.redirect('#/');
                                });
                            })
                            .error(function (data) {

                            });
                    });
                });
                return false;
            });

            $('#userTable').on('click','a[data-action-lock]',function(e){
                var id = $(this).attr('data-action-lock');
                var $tr = $(this).closest('tr');
                console.log("锁定 " + id);
                var options = {
                    method: 'post',
                    data: { id: id },
                    url: "/user/lockuser"
                };
                $.ajax(options).success(function (data) {
                    $tr.replaceWith(data);
                }).fail(function (jqXHR, status,error) {
                    console.log(error);
                });
            });
            $('#userTable').on('click', 'a[data-action-unlock]', function (e) {
                var id = $(this).attr('data-action-unlock');
                var $tr = $(this).closest('tr');
                console.log("锁定 " + id);
                var options = {
                    method: 'post',
                    data: { id: id },
                    url: "/user/unlockuser"
                };
                $.ajax(options).success(function (data) {
                    $tr.replaceWith(data);
                }).fail(function (jqXHR, status, error) {
                    console.log(error);
                });
            });

          
        });
        app.run('#/');
    });
