using Mirai.Net.Data.Messages;
using Mirai.Net.Data.Messages.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQAPI.Message
{
    public class AtAll : IMessage
    {
        public AtAll()
        {
        }

        public IMessage.Type MsgType => IMessage.Type.AtAll;

        public MessageBase ToMessage() => new AtAllMessage();

        public string ToPlainMessage() => "";
    }
}
