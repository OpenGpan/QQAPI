

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyBot.Message;

namespace QQAPI.LLOneBot.Message
{
    public class Face : IMessage
    {
        public int ID;
        public Face(int ID)
        {
            this.ID = ID;
        }
        public Face(FaceMessage msg)
        {
            ID = Convert.ToInt32(msg.Data.Id);
        }
        public IMessage.Type MsgType => IMessage.Type.Face;

        public MessageBase ToMessage() => new FaceMessage(ID);

        public string ToPlainMessage() => "";
    }
}
