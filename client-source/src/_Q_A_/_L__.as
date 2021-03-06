﻿// Decompiled by AS3 Sorcerer 1.99
// http://www.as3sorcerer.com/

//_Q_A_._L__

package _Q_A_ {
import Frames.Frame;
import Frames.TextInput;

import _qN_.Account;

import _zo._8C_;
import _zo._mS_;

import com.company.PhoenixRealmClient.appengine._0B_u;
import com.company.PhoenixRealmClient.parameters.Parameters;
import com.company.PhoenixRealmClient.ui.TextButton;
import com.company.util.keyboardKeys;

import flash.events.Event;
import flash.events.KeyboardEvent;
import flash.events.MouseEvent;

internal class _L__ extends Frame {

    public function _L__() {
        super("Sign in", "Cancel", "Sign in", "/signIn");
        this._xb = new TextInput("Username", false, "");
        addTextInputBox(this._xb);
        this.password_ = new TextInput("Password", true, "");
        addTextInputBox(this.password_);
        this._5K_ = new TextButton(12, false, "Forgot your password?  Click here");
        addTextButton(this._5K_);
        this._static = new TextButton(12, false, "New user?  Click here to Register");
        addTextButton(this._static);
        Button1.addEventListener(MouseEvent.CLICK, this.onCancel);
        Button2.addEventListener(MouseEvent.CLICK, this._q5);
        this._5K_.addEventListener(MouseEvent.CLICK, this._08Y_);
        this._static.addEventListener(MouseEvent.CLICK, this._mO_);
        this.addEventListener(Event.ADDED_TO_STAGE, this.onAdded);
        this.addEventListener(Event.REMOVED_FROM_STAGE, this.onRemoved);
    }
    public var _xb:TextInput;
    public var password_:TextInput;
    public var _5K_:TextButton;
    public var _static:TextButton;

    private function onCancel(_arg1:MouseEvent):void {
        dispatchEvent(new _nJ_(_nJ_.DONE));
    }

    private function _q5(_arg1:MouseEvent):void {
        if (this._xb.text() == "") {
            this._xb._0B_T_("Not a valid username");
            return;
        }
        if (this.password_.text() == "") {
            this.password_._0B_T_("Password too short");
            return;
        }
        var _local2:_0B_u = new _0B_u(Parameters._fK_(), "/account", true);
        _local2.addEventListener(_8C_.GENERIC_DATA, this._G_L_);
        _local2.addEventListener(_mS_.TEXT_ERROR, this._V_5);
        _local2.sendRequest("verify", {
            "guid": this._xb.text(),
            "password": this.password_.text()
        });
        setAllButtonsGray();
    }

    private function _G_L_(_arg1:_8C_):void {
        //GA.global().trackEvent("account", "signedIn");
        Account._get().modify(this._xb.text(), this.password_.text(), null);
        dispatchEvent(new _nJ_(_nJ_._tp));
    }

    private function _V_5(_arg1:_mS_):void {
        this.password_._0B_T_(_arg1.text_);
        setAllButtonsWhite();
    }

    private function _08Y_(_arg1:MouseEvent):void {
        dispatchEvent(new _nJ_(_nJ_._lS_));
    }

    private function _mO_(_arg1:MouseEvent):void {
        dispatchEvent(new _nJ_(_nJ_._G__));
    }

    private function onAdded(e:Event):void {
        this.addEventListener(KeyboardEvent.KEY_DOWN, this.onKeyPressed);
    }

    private function onRemoved(e:Event):void {
        this.removeEventListener(KeyboardEvent.KEY_DOWN, this.onKeyPressed);
    }

    private function onKeyPressed(e:KeyboardEvent):void {

        if (e.keyCode == keyboardKeys.ENTER) {
            this._q5(null);
        }
    }

}
}//package _Q_A_

