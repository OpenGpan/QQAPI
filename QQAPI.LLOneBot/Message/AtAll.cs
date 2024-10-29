

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyBot.Message;

namespace QQAPI.LLOneBot.Message
{
    public class AtAll : IMessage
    {
        public AtAll()
        {
        }

        public IMessage.Type MsgType => IMessage.Type.AtAll;

        public MessageBase ToMessage() => new AtMessage(0);

        public string ToPlainMessage() => "";
    }
}
