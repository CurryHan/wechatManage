
(function(){
    'use strict' 

var Sensing.Confirm=function(txt,f){
    var html='<div class="weui_dialog_confirm">'+
                '<div class="weui_mask"></div>'+
                '<div class="weui_dialog">'+
                    '<div class="weui_dialog_hd"><strong class="weui_dialog_title">提示</strong></div>'+
                    '<div class="weui_dialog_bd">'+txt+'</div>'+
                    '<div class="weui_dialog_ft">'+
                        '<a href="javascript:;" class="weui_btn_dialog default cancel">取消</a>'+
                        '<a href="javascript:;" class="weui_btn_dialog primary confirm">确定</a>'+
                    '</div>'+
                '</div>'+
            '</div>';


};



})();