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
