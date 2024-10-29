using QQAPI.LLOneBot.Message;
using UnifyBot.Receiver.MessageReceiver;
using static QQAPI.LLOneBot.Reply.IReply;

namespace QQAPI.LLOneBot.Reply
{
    public class Friend : IReply
    {
        public long ID { get; set; }
        public string Name { get; set; }
        PrivateReceiver? receiver = null;
        QQBot? bot = null;
        public Friend(long id, QQBot bot)
        {
            ID = id;
            Name = "";
            this.bot = bot;
        }
        public Friend(PrivateReceiver receiver)
        {
            ID = Convert.ToInt64(receiver.SenderQQ);
            Name = receiver.Sender.Nickname;
            this.receiver = receiver;
        }

        public ReplyType Type => ReplyType.Group;
        public async Task MessageSend(Messages messages)
        {
            if (receiver != null)
                await receiver.SendMessage(messages.ToMessageChain());
            else
                await bot?.GetBot().SendPrivateMessage(ID, messages.ToMessageChain());
        }
        public async Task MessageReplay(Messages messages)
        {
            if (receiver != null)
                messages.Add(new Quote(receiver.MessageId));
            await MessageSend(messages);
        }
    }
}
