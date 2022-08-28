using Mirai.Net.Data.Messages.Receivers;
using Mirai.Net.Sessions.Http.Managers;
using Mirai.Net.Utils.Scaffolds;
using QQAPI.Message;
using static QQAPI.Reply.IReply;

namespace QQAPI.Reply
{
    public class Temp : IReply
    {
        public long ID { get; set; }
        public long GroupID;
        public string Name { get; set; }
        TempMessageReceiver? receiver = null;
        public Temp(long id, long groupiD)
        {
            ID = id;
            GroupID = groupiD;
        }
        public Temp(TempMessageReceiver receiver)
        {
            ID = Convert.ToInt64(receiver.Sender.Id);
            Name = receiver.Sender.Name;
            GroupID = Convert.ToInt64(receiver.GroupId);
            this.receiver = receiver;
        }

        public ReplyType Type => ReplyType.Group;
        public async Task MessageSend(Messages messages)
        {
            if (receiver != null)
                await receiver.SendMessageAsync(messages.ToMessageChain());
            else
                await MessageManager.SendTempMessageAsync(ID.ToString(),GroupID.ToString(), messages.ToMessageChain());
        }
        public async Task MessageReplay(Messages messages)
        {
            if (receiver != null)
                await receiver.QuoteMessageAsync(messages.ToMessageChain());
            else
                await MessageManager.SendTempMessageAsync(ID.ToString(), GroupID.ToString(), messages.ToMessageChain());
        }
    }
}
