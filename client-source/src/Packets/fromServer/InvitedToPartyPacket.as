﻿// Decompiled by AS3 Sorcerer 1.99
// http://www.as3sorcerer.com/

//_011._039

package Packets.fromServer {
import flash.utils.IDataInput;

public class InvitedToPartyPacket extends SvrPacketError {

    public function InvitedToPartyPacket(_arg1:uint) {
        super(_arg1);
    }
    public var name_:String;
    public var partyID_:int;

    override public function parseFromInput(_arg1:IDataInput):void {
        this.name_ = _arg1.readUTF();
        this.partyID_ = _arg1.readInt();
    }

    override public function toString():String {
        return (formatToString("INVITEDTOGUILD", "name_", "partyID_"));
    }

}
}//package _011

