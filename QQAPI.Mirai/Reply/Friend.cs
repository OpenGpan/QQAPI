using Mirai.Net.Data.Messages.Receivers;
using Mirai.Net.Sessions.Http.Managers;
using Mirai.Net.Utils.Scaffolds;
using QQAPI.MiraiNET.Message;
using static QQAPI.MiraiNET.Reply.IReply;

namespace QQAPI.MiraiNET.Reply
{
    public class Friend : IReply
    {
        public long ID { get; set; }
        public string Name { get; set; }
        FriendMessageReceiver? receiver = null;
        public Friend(long id)
        {
            ID = id;
            Name = "";
        }
        public Friend(FriendMessageReceiver receiver)
        {
            ID = Convert.ToInt64(receiver.FriendId);
            Name = receiver.FriendName;           
            this.receiver = receiver;
        }

        public ReplyType Type => ReplyType.Group;
        public async Task MessageSend(Messages messages)
        {
            if (receiver != null)
                await receiver.SendMessageAsync(messages.ToMessageChain());
            else
                await MessageManager.SendFriendMessageAsync(ID.ToString(), messages.ToMessageChain());
        }
        public async Task MessageReplay(Messages messages)
        {
            if (receiver != null)
                await receiver.QuoteMessageAsync(messages.ToMessageChain());
            else
                await MessageManager.SendFriendMessageAsync(ID.ToString(), messages.ToMessageChain());
        }
    }
}
