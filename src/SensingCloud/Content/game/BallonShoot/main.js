window.onload = function () {
    cc.game.onStart = function () {

        //cc.path.pathStr = "cocos2d";
        // Pass true to enable retina display, disabled by default to improve performance

        //cc.view.enableRetina(false);
        // Adjust viewport meta
        cc.view.adjustViewPort(true);
        // Setup the resolution policy and design resolution size
        cc.view.setDesignResolutionSize(1920, 1080, cc.ResolutionPolicy.NO_BORDER);
        // The game will be resized when browser size change
        cc.view.resizeWithBrowserSize(true);

        //load resources
        cc.LoaderScene.preload(g_mainmenu, function () {
            // var MyScene = cc.Scene.extend({
            //     onEnter: function () {
            //         this._super();
            //         var size = cc.director.getWinSize();
            //         var bg = cc.Sprite.create(res.BackGround_jpg);
            //         bg.setPosition(size.width / 2, size.height / 2);
            //         this.addChild(bg);
            //
            //         var sprite = cc.Sprite.create(res.Logo_png);
            //         sprite.setPosition(size.width / 2, size.height-sprite.height/2);
            //         sprite.setScale(0.8);
            //         this.addChild(sprite, 0);
            //
            //         sprite.runAction(cc.spawn(cc.rotateBy(1, 360, 360), cc.sequence(cc.scaleTo(1, 2), cc.scaleTo(1, 1))).repeatForever());
            //
            //         var label = cc.LabelTTF.create("William Wu", "Arial", 40);
            //         label.setPosition(size.width / 2, size.height/2);
            //         this.addChild(label, 1);
            //     }
            // });
            // cc.spriteFrameCache.addSpriteFrames(res.displayTexturePack_plist);
            cc.spriteFrameCache.addSpriteFrames(res.main_plist);
            cc.director.runScene(SysMenu.scene());
            //cc.director.runScene(new StartScene());
        }, this);
    };
    cc.game.run("gameCanvas");
};