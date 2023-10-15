using Mirai.Net.Data.Messages;
using Mirai.Net.Data.Messages.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQAPI.MiraiNET.Message
{
    public class Plain : IMessage
    {
        public string message;
        public Plain(string message)
        {
            this.message = message;
        }
        public Plain(PlainMessage message)
        {
            this.message = message.Text;
        }

        public IMessage.Type MsgType => IMessage.Type.Plain;

        public MessageBase ToMessage() => new PlainMessage(message);

        public string ToPlainMessage() => message;
    }
}
