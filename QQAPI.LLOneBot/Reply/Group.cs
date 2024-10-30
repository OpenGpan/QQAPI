
using QQAPI.LLOneBot.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyBot;
using UnifyBot.Message;
using UnifyBot.Model;
using UnifyBot.Receiver.MessageReceiver;
using static QQAPI.LLOneBot.Reply.IReply;
using Messages = QQAPI.LLOneBot.Message.Messages;

namespace QQAPI.LLOneBot.Reply
{
    public class Group : IReply
    {
        QQBot? bot = null;
        public long ID { get; set; }
        public long SenderID;
        public string Name { get; set; }
        public bool IsAdmin = false;
        GroupInfo? group = null;

        public Group(long id, QQBot? bot)
        {
            ID = id;
            this.bot = bot;
            var g = bot?.Groups().Find(x => x.ID == id);
            if (g != null)
            {
                group = g.group;
                Name = g.Name;
                IsAdmin = g.IsAdmin;
            }
        }
        long? replyid;
        public Group(GroupReceiver groupr)
        {
            ID = Convert.ToInt64(groupr.GroupQQ);
            replyid = groupr.MessageId;
            group = groupr.Group;
            SenderID = Convert.ToInt64(groupr.SenderQQ);
            Name = group.GroupName;
            Name = group.GroupName;
            var r = group.Members.Find(x => x.QQ == groupr.BotQQ)?.Role;
            IsAdmin = r == Permissions.Owner || r == Permissions.Admin;
        }
        public Group(GroupInfo group, long botqq)
        {
            this.group = group;
            ID = Convert.ToInt64(group.GroupQQ);
            Name = group.GroupName;
            var r = group.Members.Find(x => x.QQ == botqq)?.Role;
            IsAdmin = r == Permissions.Owner || r == Permissions.Admin;
        }


        public MessagesType Type => MessagesType.Group;

        public async Task MessageSend(Messages messages)
        {
            if (group != null)
                await group.SendMessage(messages.ToMessageChain());
        }
        public async Task MessageReplay(Messages messages)
        {
            if (group != null)
                if (replyid != null)
                {
                    messages.Add(new Quote(replyid.Value));
                    await group.SendMessage(messages.ToMessageChain());
                }
                else
                    await group.SendMessage(messages.ToMessageChain());
        }

    }
}
