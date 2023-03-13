//====================================================================================
//
// LionX Touch UI Icons B Gone
//
//====================================================================================
/*:
* @plugindesc removes the touch UI and fixes the window padding.
* @author LionX
* 
* @target MZ

* @help 
* Place anywhere and enjoy.

*/
//====

Scene_Map.prototype.createButtons = function() {
    if (ConfigManager.touchUI) {
    }
};

Scene_Battle.prototype.updateCancelButton = function() {
    if (this._cancelButton) {
        this._cancelButton.visible = false 
    }
};

Scene_MenuBase.prototype.createCancelButton = function() {
    this._cancelButton = new Sprite_Button("cancel");
    this._cancelButton.x = Graphics.boxWidth - this._cancelButton.width - 4;
    this._cancelButton.y = this.buttonY();
};

Scene_MenuBase.prototype.mainAreaTop = function() {
    if (!this.isBottomHelpMode()) {
        return 0
    } else if (this.isBottomButtonMode()) {
        return 0;
    } else {
        return 0
    }
};

 Scene_MenuBase.prototype.mainAreaHeight = function() {
    return Graphics.boxHeight;
}; 

Scene_MenuBase.prototype.updatePageButtons = function() {
    if (this._pageupButton && this._pagedownButton) {
        const enabled = this.arePageButtonsEnabled();
        this._pageupButton.visible = false;
        this._pagedownButton.visible = false;
    }
};