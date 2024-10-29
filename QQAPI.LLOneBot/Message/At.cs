

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyBot.Message;

namespace QQAPI.LLOneBot.Message
{
    public class At : IMessage
    {
        public long qq;
        public At(long qq)
        {
            this.qq = qq;
        }
        public At(AtMessage msg)
        {
            qq = Convert.ToInt64(msg.Data.QQ);
        }

        public IMessage.Type MsgType => IMessage.Type.At;

        public MessageBase ToMessage() => new AtMessage(qq.ToString());

        public string ToPlainMessage() => "";
    }
}
