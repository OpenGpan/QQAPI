using Mirai.Net.Data.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirai.Net.Data.Messages.Concretes;

namespace QQAPI.MiraiNET.Message
{
    public class Quote : IMessage
    {
        public int id;
        public long groupid;
        public Quote(int id, long groupid)
        {
            this.id = id;
            this.groupid = groupid;
        }
        public Quote(QuoteMessage msg)
        {
            id = Convert.ToInt32(msg.MessageId);
            groupid = Convert.ToInt64(msg.GroupId);
        }
        public IMessage.Type MsgType => IMessage.Type.Quote;

        public MessageBase ToMessage() => new QuoteMessage()
        {
            MessageId = id.ToString(),
            GroupId = groupid.ToString(),
        };

        public string ToPlainMessage() => "";

    }
}
