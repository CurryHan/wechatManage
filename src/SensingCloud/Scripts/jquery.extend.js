$.extend({
    alert: function (message) {
        layer.alert(message);
    },
    ok: function (message) {
        layer.alert(message, { icon: 1 });
    },
    error: function (message) {
        layer.alert(message, { icon: 2 });
    },
    warning: function (message) {
        layer.alert(message, { icon: 7 });
    },
    msg: function (message) {
        layer.msg(message, {
            time: 1500
        });
    },
    loading: function () {
        layer.load();
    },
    closeLoading: function () {
        layer.closeAll('loading');
    },
    confirm: function (message, callback) {
        layer.confirm(message, function (index) {
            layer.close(index);
            if (callback) {
                callback();
            }
        });
    },
    modal: function (url, title, width, height) {
        width = width ? width + "px" : "70%";
        height = height ? height + "px" : "80%";
        if ($(window).width() <= 768) {
            width = "100%";
            height = "100%";
        }
        layer.open({
            type: 2,
            title: title,
            content: url,
            maxmin: true,
            area: [width, height],
            moveOut: true,
            move: false
        });
    },
    closeModal: function () {
        var index = parent.layer.getFrameIndex(window.name);
        parent.layer.close(index);
    }
});
