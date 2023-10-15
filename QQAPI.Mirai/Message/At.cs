using Mirai.Net.Data.Messages;
using Mirai.Net.Data.Messages.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQAPI.MiraiNET.Message
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
            qq = Convert.ToInt64(msg.Target);
        }

        public IMessage.Type MsgType => IMessage.Type.At;

        public MessageBase ToMessage() => new AtMessage(qq.ToString());

        public string ToPlainMessage() => "";
    }
}
