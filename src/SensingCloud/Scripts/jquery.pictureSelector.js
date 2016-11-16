
/**上传图片插件，插件使用html5新特性实现图片预览
使用示例 ：   $('#file').pictureSelector();
 **/
 $(function () {
            $.fn.pictureSelector = function () {
                console.log(this);
                var $file = this;
                var $parent = this.closest('[data-target-parent]');
                var $preview = $parent.find('img[data-target-preview]');
                var $button = $parent.find('button[data-action-select]');
                $button.click(function (e) {
                    $file.click();
                });
                $preview.click(function (e) {
                    $file.click();
                });

                var oFReader = new FileReader(), rFilter = /^(?:image\/bmp|image\/cis\-cod|image\/gif|image\/ief|image\/jpeg|image\/jpeg|image\/jpeg|image\/pipeg|image\/png|image\/svg\+xml|image\/tiff|image\/x\-cmu\-raster|image\/x\-cmx|image\/x\-icon|image\/x\-portable\-anymap|image\/x\-portable\-bitmap|image\/x\-portable\-graymap|image\/x\-portable\-pixmap|image\/x\-rgb|image\/x\-xbitmap|image\/x\-xpixmap|image\/x\-xwindowdump)$/i;

                oFReader.onload = function (oFREvent) {
                    $preview.attr("src", oFREvent.target.result);
                };

                $file.change(function () {
                    var oFile = this.files[0];
                    if (!rFilter.test(oFile.type)) { alert("You must select a valid image file!"); return; }
                    oFReader.readAsDataURL(oFile);
                });
            };

        });