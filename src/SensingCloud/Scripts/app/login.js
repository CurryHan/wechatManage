jQuery(function ($) {

    $(document).on('click', '.toolbar a[data-target]', function (e) {
        e.preventDefault();
        var target = $(this).data('target');
        $('.widget-box.visible').removeClass('visible'); //hide others
        $(target).addClass('visible'); //show target
    });
    $("a[data-target='#forgot-box']").click(function () {
        $('#email-confirm-pane').hide();
        $('#phone-confirm-pane').hide();
        $('#form-pane').show();
    });

    $("#sendemailform").submit(function (e) {
        e.preventDefault();
         //$('#email-confirm-pane').show();
         //$('#form-pane').hide();
        $.post('/Account/ForgotPassword', $(this).serialize()).success(function (data) {
            if (data == "NotMatch") {
                $("#phone-confirm-pane").show();
                $("#phone-confirm-pane").text("没有查询到该邮箱的注册信息！");
            } else if (data == "Success") {
                $("#phone-confirm-pane").show();
                $("#phone-confirm-pane").text("发送成功，请到邮箱重置密码!");
            }
        }).fail(function (jqxhr, textStatus, error) {
            console.log("error");
        });
        return false;
    });

    //$("#sendcodebtn").click(function () {
    //    var Url = "/Account/ForgotPassword";
    //    var options = {
    //        method: 'get',
    //        data: { PhoneNumber: $('input[name=Phone]').val() }
    //    };
    //    $.ajax(Url, options).success(function (data) {
    //        alert("success");
    //    });
    //});
        
    function daojishi()
    {
        var count = 60;
        var countdown = setInterval(CountDown, 1000);
        function CountDown() {
            $("#sendcodebtn").attr("disabled", true);
            $("#sendcodebtn").text( count + " 秒后重新获取!");
            if (count == 0) {
                $("#sendcodebtn").text("重新获取验证码").removeAttr("disabled");
                clearInterval(countdown);
            }
            count--;
        }
    }

    $("#sendphoneform").submit(function (e) {
        e.preventDefault();
        $("#phone-confirm-pane").text("");

        $.get('/Account/ForgotPassword', $(this).serialize()).success(function (data) {
            if (data == "NotMatch") {
                $("#phone-confirm-pane").show();
                $("#phone-confirm-pane").text("没有查询到该手机号的注册信息！");
            } else if (data == "Success")
            {
                $("#phone-confirm-pane").show();
                $("#phone-confirm-pane").text("发送成功，请输入验证码！");
                daojishi();
            }
        })
        .fail(console.log("error"));

        //$('#phone-confirm-pane').show();
        //$('#form-pane').hide();
        //$.post('/Account/ForgotPassword', $(this).serialize()).success(function () {

        //}).fail(function (jqxhr, textStatus, error) {
        //    console.log("error");
        //});
        return false;
    }); 
    $("#cancelnewpasswordbtn").click(function () {
        $("#forgot-box").addClass('visible');
        $("#editpassword-box").removeClass('visible');
    });

    //$('#sendcodebtn').click(function () {
    //    var count = 10;
    //    var countdown = setInterval(CountDown, 1000);
    //    function CountDown() {
    //        $("#sendcodebtn").attr("disabled", true);
    //        $("#sendcodebtn").text("您在" + count + " 秒后可以再次获取!");
    //        if (count == 0) {
    //            $("#sendcodebtn").val("重新获取验证码").removeAttr("disabled");
    //            clearInterval(countdown);
    //        }
    //        count--;
    //    }
    //});

    $("#VerifyPhoneCodeForm").submit(function (e) {
        e.preventDefault();
        var datastr =$.param({PhoneNumber:$("input[name='Phone']").val()}) +"&"+ $(this).serialize();
        console.log(datastr);
        $.get('/Account/VerifyPhoneCode', datastr)
        .success(function (data) {
            if (data == "Success") {
                $("#forgot-box").removeClass('visible');
                $("#editpassword-box").addClass('visible');
            } else if (data == "NotMatch") {
                $("#phone-confirm-pane").show();
                $("#phone-confirm-pane").text("没有查询到该手机号的注册信息！");
            } else
            {
                $("#phone-confirm-pane").show();
                $("#phone-confirm-pane").text("验证失败！");
            }
        }).fail(function (jqXHR, textStatus, error) {
            alert();
        });
        return false;
    });

    

    $("#resetpasswordform").submit(function (e) {
        e.preventDefault();
        var txtpwd = $("#NewPassword").val().trim();
        var txtpwd2 = $("#Confirpassword").val().trim();
        if (txtpwd != txtpwd2) {
            $("#confirm-pwd").show();
            $("#confirm-pwd").text("两次密码不一致");
            return false;
        }
        var datastr = $.param({ PhoneNumber: $("input[name='Phone']").val() }) + "&" + $.param({ newpassword: $("#NewPassword").val() });
        $.get('/Account/ResetPasswordWithPhone', datastr)
        .success(function (data) {
            if (data == "Success") {
                $("#confirm-pwd").show();
                $("#confirm-pwd").text("重置密码成功，请返回到登陆页面登陆！");
            } else if (data == "NotMatch") {
                $("#confirm-pwd").show();
                $("#confirm-pwd").text("重置密码失败，没有查询到该用户！");
            }
        }).fail(function (jqXHR, textStatus, error) {
            alert();
        });
        return false;
    });

    $(".weixin").hover(function () {
        $(".weixincode").show();
        $(this).css({ "background": "url('../Content/img/weixin_light.png')" });
    }, function () {
        $(".weixincode").hide();
        $(this).css({ "background": "url('../Content/img/weixin.png')" });
    });
    $(".sina").hover(function () {
        $(".sinacode").show();
    }, function () {
        $(".sinacode").hide();
    });
 });