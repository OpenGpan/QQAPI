using Mirai.Net.Data.Messages.Receivers;
using Mirai.Net.Sessions.Http.Managers;
using Mirai.Net.Utils.Scaffolds;
using QQAPI.MiraiNET.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QQAPI.MiraiNET.Reply.IReply;

namespace QQAPI.MiraiNET.Reply
{
    public class Group : IReply
    {
        public long ID { get; set; }
        public long SenderID;
        public string Name { get; set; }
        public bool IsAdmin = false;
        GroupMessageReceiver? group = null;
        public Group(long id)
        {
            ID = id;
            Name = "";
        }
        public Group(GroupMessageReceiver group)
        {
            ID = Convert.ToInt64(group.GroupId);
            Name = group.GroupName;
            IsAdmin = group.BotPermission != Mirai.Net.Data.Shared.Permissions.Member;
            SenderID = Convert.ToInt64(group.Sender.Id);
            this.group = group;
        }
        public Group(Mirai.Net.Data.Shared.Group group)
        {
            ID = Convert.ToInt64(group.Id);
            Name = group.Name;
            IsAdmin = group.Permission == Mirai.Net.Data.Shared.Permissions.Member;
        }

        public ReplyType Type => ReplyType.Group;

        public async Task MessageSend(Messages messages)
        {
            if (group != null)
                await group.SendMessageAsync(messages.ToMessageChain());
            else
                await MessageManager.SendGroupMessageAsync(ID.ToString(), messages.ToMessageChain());
        }
        public async Task MessageReplay(Messages messages)
        {
            if (group != null)
                await group.QuoteMessageAsync(messages.ToMessageChain());
            else
                await MessageManager.SendGroupMessageAsync(ID.ToString(), messages.ToMessageChain());
        }

    }
}
