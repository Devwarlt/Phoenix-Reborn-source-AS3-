﻿// Decompiled by AS3 Sorcerer 1.99
// http://www.as3sorcerer.com/

//_00g.WebAccount

package _00g {
import _0L_C_.GuestGuardDialog;

import _Q_A_._02R_;
import _Q_A_._0A_c;
import _Q_A_._ak;
import _Q_A_._jz;

import _qN_.Account;
import _qN_._9j;

import com.company.PhoenixRealmClient.appengine._02k;
import com.company.PhoenixRealmClient.parameters.Parameters;
import com.company.PhoenixRealmClient.util.GUID;

import flash.display.Sprite;
import flash.display.Stage;
import flash.external.ExternalInterface;
import flash.net.SharedObject;

public class WebAccount extends Account {

    public static const _000:String = "phoenixrealm";
    private static const _oF_:String = "";
    private static const _03R_:String = "phoenixrealm";

    public function WebAccount() {
    }
    private var guid_:String = null;
    private var password_:String = null;
    private var _8U_:String = "";

    override public function guid():String {
        return (this.guid_);
    }

    override public function password():String {
        return ((((this.password_) == null) ? "" : this.password_));
    }

    override public function credentials():Object {
        return ({
            "guid": this.guid(),
            "password": this.password()
        });
    }

    override public function isRegistered():Boolean {
        return (!((this.password() == "")));
    }

    override protected function internalLoad(_arg1:Stage, _arg2:Function):void {
        var _local3:SharedObject;
        this.guid_ = null;
        this.password_ = null;
        try {
            _local3 = SharedObject.getLocal("phoenixrealm", "/");
            if (_local3.data.hasOwnProperty("GUID")) {
                this.guid_ = _local3.data["GUID"];
            }
            if (_local3.data.hasOwnProperty("Password")) {
                this.password_ = _local3.data["Password"];
            }
        } catch (error:Error) {
        }
        if (this.guid_ == null) {
            this.modify(GUID.create(), null, null);
        }
        (_arg2());
    }

    override public function modify(_arg1:String, _arg2:String, _arg3:String):void {
        var _local4:SharedObject;
        this.guid_ = _arg1;
        this.password_ = _arg2;
        try {
            _local4 = SharedObject.getLocal("phoenixrealm", "/");
            _local4.data["GUID"] = this.guid_;
            _local4.data["Password"] = this.password_;
            _local4.flush();
        } catch (error:Error) {
        }
    }

    override public function clear():void {
        this.modify(GUID.create(), null, null);
        Parameters._hk = true;
        Parameters.ClientSaveData.charIdUseMap = {};
        Parameters.save();
    }

    override public function reportIntStat(_arg1:String, _arg2:int):void {
    }

    override public function newAccountText():_9j {
        return (new _jz());
    }

    override public function newAccountManagement():Sprite {
        return (new _ak(false));
    }

    override public function showInGameRegister(_arg1:Stage):void {
        var _local2:_0A_c = new _0A_c();
        _arg1.addChild(_local2);
    }

    override public function cacheOffers():void {
        _02k._U_t("/credits", null);
    }

    override public function showMoneyManagement(_arg1:Stage):void {
        if (!this.isRegistered()) {
            _arg1.addChild(new GuestGuardDialog(("In order to buy Gold " + ", you must be a registered user.")));
            return;
        }
        _arg1.addChild(new _02R_());
    }

    override public function gameNetworkUserId():String {
        return (_oF_);
    }

    override public function gameNetwork():String {
        return (_000);
    }

    override public function playPlatform():String {
        return (_03R_);
    }

    override public function entrytag():String {
        return (this._8U_);
    }

}
}//package _00g

