using Mirai.Net.Data.Messages;
using Mirai.Net.Data.Messages.Concretes;
using Mirai.Net.Data.Messages.Receivers;
using Mirai.Net.Utils.Scaffolds;
using QQAPI.Reply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQAPI.Message
{
    public class Messages : List<IMessage>
    {
        public int MessageID;
        public DateTime Time;
        public IReply Reply;
        public Messages(IReply reply)
        {
            Reply = reply;
        }
        public Messages(GroupMessageReceiver r)
        {
            Reply = new Group(r);
            LoadMessage(r.MessageChain);
        }
        public Messages(FriendMessageReceiver r)
        {
            Reply = new Friend(r);
            LoadMessage(r.MessageChain);
        }
        public Messages(TempMessageReceiver r)
        {
            Reply = new Temp(r);
            LoadMessage(r.MessageChain);
        }
        public void LoadMessage(MessageChain message)
        {
            foreach (MessageBase? msg in message)
            {
                switch (msg.Type)
                {
                    case Mirai.Net.Data.Messages.Messages.Plain:
                        Add(new Plain((PlainMessage)msg));
                        break;
                    case Mirai.Net.Data.Messages.Messages.Image:
                    case Mirai.Net.Data.Messages.Messages.FlashImage:
                        Add(new Image((ImageMessage)msg));
                        break;
                    case Mirai.Net.Data.Messages.Messages.At:
                        Add(new At((AtMessage)msg));
                        break;
                    case Mirai.Net.Data.Messages.Messages.AtAll:
                        Add(new AtAll());
                        break;
                    case Mirai.Net.Data.Messages.Messages.Face:
                        Add(new Face((FaceMessage)msg));
                        break;
                    case Mirai.Net.Data.Messages.Messages.Json:
                        Add(new Json((JsonMessage)msg));
                        break;
                    case Mirai.Net.Data.Messages.Messages.Quote:
                        Add(new Quote((QuoteMessage)msg));
                        break;
                    case Mirai.Net.Data.Messages.Messages.Voice:
                        Add(new Voice((VoiceMessage)msg));
                        break;
                    case Mirai.Net.Data.Messages.Messages.Source:
                        MessageID = Convert.ToInt32(((SourceMessage)msg).MessageId);
                        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                        Time = dtStart.Add(new TimeSpan(long.Parse(((SourceMessage)msg).Time + "0000000")));
                        break;
                    default:
                        Add(new Other(msg));
                        break;
                }
            }
        }
        public MessagesType Type;
        public enum MessagesType
        {
            Temp,
            Friend,
            Group
        }
        public string ToPlainString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IMessage message in this)
                sb.Append(message.ToPlainMessage());
            return sb.ToString();
        }

        public MessageChain ToMessageChain()
        {
            var messageChain = new MessageChainBuilder();
            foreach (IMessage message in this)
                messageChain.Append(message.ToMessage());
            return messageChain.Build();
        }

        public void MessageSend()
        {
            Reply.MessageSend(this);
        }
        public void MessageReplay()
        {
            Reply.MessageReplay(this);
        }
        public Messages ToSend() => new Messages(Reply);


        public Messages AddPlainMessage(string message)
        {
            Add(new Plain(message));
            return this;
        }
        public Messages AddImageMessage(string image)
        {
            Add(new Image(image));
            return this;
        }
        public Messages AddAtMessage(long qq)
        {
            Add(new At(qq));
            return this;
        }
    }
}
