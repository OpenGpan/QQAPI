using QQAPI.LLOneBot.Message;
using UnifyBot;
using UnifyBot.Receiver.MessageReceiver;
using static QQAPI.LLOneBot.Reply.IReply;

namespace QQAPI.LLOneBot.Reply
{
    public class Temp : IReply
    {
        public long ID { get; set; }
        public string Name { get; set; }
        QQBot bot;
        MessageReceiver receiver;

        public Temp(MessageReceiver receiver, QQBot bot)
        {
            ID = Convert.ToInt64(receiver.SenderQQ);
            this.receiver = receiver;
        }

        public MessagesType Type => MessagesType.Group;
        public async Task MessageSend(Messages messages)
        {
            bot.GetBot().SendPrivateMessage(ID, messages.ToMessageChain());
        }
        public async Task MessageReplay(Messages messages)
        {
            messages.Add(new Quote(receiver.MessageId));
            await MessageSend(messages);
        }
    }
}
