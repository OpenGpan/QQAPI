



using LinePutScript.Converter;
using QQAPI.LLOneBot.Reply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyBot;
using UnifyBot.Message;
using UnifyBot.Message.Chain;
using UnifyBot.Receiver;
using UnifyBot.Receiver.MessageReceiver;
using static QQAPI.LLOneBot.Reply.IReply;

namespace QQAPI.LLOneBot.Message
{
    public class Messages : List<IMessage>
    {
        public long MessageID;
        public DateTime Time;
        public IReply Reply;
        public Messages(IReply reply)
        {
            Reply = reply;
            Type = reply.Type;
        }
        public Messages(GroupReceiver r)
        {
            Reply = new Group(r);
            MessageID = r.MessageId;
            Type = MessagesType.Group;
            LoadMessage(r.Message);
        }
        public Messages(PrivateReceiver r)
        {
            Reply = new Friend(r);
            Type = MessagesType.Friend;
            LoadMessage(r.Message);
        }
        public Messages(MessageReceiver r, QQBot bot)
        {
            Reply = new Temp(r, bot);
            Type = MessagesType.Temp;
            LoadMessage(r.Message);
        }
        public void LoadMessage(MessageChain message)
        {
            foreach (MessageBase? msg in message)
            {
                switch (msg.Type)
                {
                    case UnifyBot.Model.Messages.Text:
                        if (msg is TextMessage m) 
                            Add(new Plain(m));
                        break;
                    case UnifyBot.Model.Messages.Image:
                        Add(new Image((ImageMessage)msg));
                        break;
                    case UnifyBot.Model.Messages.At:
                        var at = (AtMessage)msg;
                        if (long.TryParse(at.Data.QQ, out var qq))
                            Add(new At(qq));
                        else
                            Add(new AtAll());
                        break;
                    case UnifyBot.Model.Messages.Face:
                        Add(new Face((FaceMessage)msg));
                        break;
                    case UnifyBot.Model.Messages.Json:
                        Add(new Json((JsonMessage)msg));
                        break;
                    case UnifyBot.Model.Messages.Reply:
                        Add(new Quote((ReplyMessage)msg));
                        break;
                    case UnifyBot.Model.Messages.Record:
                        Add(new Voice((RecordMessage)msg));
                        break;
                    default:
                        Add(new Other(msg));
                        break;
                }
            }
        }
        public MessagesType Type;
        public string ToPlainString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IMessage message in this)
                sb.Append(message.ToPlainMessage());
            return sb.ToString();
        }

        public MessageChain ToMessageChain()
        {
            var messageChain = new MessageChain();
            foreach (IMessage message in this)
                messageChain.Add(message.ToMessage());
            return messageChain;
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
