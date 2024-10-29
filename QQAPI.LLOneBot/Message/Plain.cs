

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyBot.Message;

namespace QQAPI.LLOneBot.Message
{
    public class Plain : IMessage
    {
        public string message;
        public Plain(string message)
        {
            this.message = message;
        }
        public Plain(TextMessage message)
        {
            this.message = message.Data.Text;
        }

        public IMessage.Type MsgType => IMessage.Type.Plain;

        public MessageBase ToMessage() => new TextMessage(message);

        public string ToPlainMessage() => message;
    }
}
