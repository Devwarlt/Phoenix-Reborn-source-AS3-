﻿using System.Linq;
using log4net;
using System;
using wServer.networking;
using wServer.networking.svrPackets;
using wServer.realm.entities;

namespace wServer.realm
{
    public class ChatManager
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ChatManager));

        private readonly RealmManager manager;

        public ChatManager(RealmManager manager)
        {
            this.manager = manager;
        }

        public void Say(Player src, string text)
        {
            if (src.Client.Account.Muted) return;
            if (src.Client.Account.Tag != "")
                src.Name = "[" + src.Client.Account.Tag + "] " + src.Client.Account.Name;
            src.Owner.BroadcastPacketWithIgnores(new TextPacket
            {
                Name = (src.Client.Account.Rank >= 2 ? "@" : "") + src.Name,
                ObjectId = src.Id,
                Stars = src.Stars,
                BubbleTime = 10,
                Recipient = "",
                Text = text.ToSafeText(),
                CleanText = text.ToSafeText()
            }, src);
            log.InfoFormat("[{0}({1})] <{2}> {3}", src.Owner.Name, src.Owner.Id, src.Name, text);
        }

        public void SayGuild(Player src, string text)
        {
            if (src.Client.Account.Muted) return;
            foreach (var i in src.Manager.Clients.Values.Where(i => i.Player != null).Where(i => String.Equals(src.Guild, i.Player.Guild)).Where(i => !i.Player.Ignored.Contains(src.AccountId)))
            {
                i.SendPacket(new TextPacket()
                {
                    Name = src.ResolveGuildChatName(),
                    ObjectId = src.Id,
                    Stars = src.Stars,
                    BubbleTime = 10,
                    Recipient = "*Guild*",
                    Text = text.ToSafeText(),
                    CleanText = text.ToSafeText()
                });
            }
        }

        public void SayParty(Player src, string text)
        {
            if (src.Client.Account.Muted) return;
            src.Party.SendPacket(new TextPacket
            {
                Name = src.Name,
                ObjectId = src.Id,
                Stars = src.Stars,
                BubbleTime = 10,
                Recipient = "*Party*",
                Text = text.ToSafeText(),
                CleanText = text.ToSafeText()
            }, null);
        }

        public void Announce(string text)
        {
            foreach (Client i in manager.Clients.Values)
                i.SendPacket(new TextPacket
                {
                    BubbleTime = 0,
                    Stars = -1,
                    Name = "@ANNOUNCEMENT",
                    Text = text.ToSafeText()
                });
            log.InfoFormat("<ANNOUNCEMENT> {0}", text);
        }

        public void Oryx(World world, string text)
        {
            world.BroadcastPacket(new TextPacket
            {
                BubbleTime = 0,
                Stars = -1,
                Name = "#Oryx the Mad God",
                Text = text.ToSafeText()
            }, null);
            log.InfoFormat("[{0}({1})] <Oryx the Mad God> {2}", world.Name, world.Id, text);
        }

        public void ChatBot(World world, string text)
        {
            world.BroadcastPacket(new TextPacket
            {
                BubbleTime = 0,
                Stars = -1,
                Name = "!Bot",
                Text = text.ToSafeText()
            }, null);
            log.InfoFormat("[{0}({1})] <HatBot> {2}", world.Name, world.Id, text);
        }
    }
}