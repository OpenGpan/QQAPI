
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyBot.Message;


namespace QQAPI.LLOneBot.Message
{
    public class Quote : IMessage
    {
        public long id;
        public Quote(long id)
        {
            this.id = id;
        }
        public Quote(ReplyMessage msg)
        {
            id = Convert.ToInt64(msg.Data.Id);
        }
        public IMessage.Type MsgType => IMessage.Type.Quote;

        public MessageBase ToMessage() => new ReplyMessage(id);

        public string ToPlainMessage() => "";

    }
}
