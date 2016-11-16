/***
自定义switch button
示例
    $('input[name=switcher]').switchButton({
    onLabel: '列表',
    offLabel: '表格',
    fontSize:'12px',
    width: 70,
    height: 30,
});
*/
(function ($) {
	$.fn.switchButton = function (options) {
		
                var settings = $.extend({
                    color: "white",
                    backgroundColor: "#468fcc",
                    width: 70,
                    height:28
                }, options);
				this.each(function () {
			        var self = $(this);
					var $parent = $('<label class="switch"></label>').css({
						position:'relative',
						display:'block',
						backgroundColor: settings.backgroundColor,
						width: settings.width,
						height:settings.height
					});
					var $t = $('<span class="t">III</span>').css({
                		color: 'rgb(147,147,147)',
                		cursor: 'pointer',
                		position: 'absolute',
                		top: '2px',
                		left: '2px',
						textAlign:'center',
						backgroundColor: settings.color,
						width: settings.width / 2,
						height: settings.height - 4,
						lineHeight:(settings.height - 4) + 'px'
					});
					var cls_lbl = {
                		textAlign:'center',
                		display:'table-cell',
                		color: settings.color,
                		fontSize:settings.fontSize,
                		width:settings.width/2,
                		height: settings.height,
                		lineHeight:settings.height + 'px'
					};
					var $lbl_1 = $('<span class="lbl_1">' + settings.onLabel + '</span>').css(cls_lbl);
					var $lab_2 = $('<span class="lbl_2">' + settings.offLabel + '</span>').css(cls_lbl);

					self.change(function (e) {
						if (self.is(':checked')) {
							$t.animate({ left: (settings.width / 2 - 2) + 'px' });
						} else {
							$t.animate({ left: '2px' });
						}
					});
					self.wrap($parent)
						.after($t)
						.after($lbl_1)
						.after($lab_2);

                });
                return this;

            };
  }(jQuery));