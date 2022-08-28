using Mirai.Net.Data.Messages;
using Mirai.Net.Data.Messages.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQAPI.Message
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
            ID = Convert.ToInt32(msg.FaceId);
        }
        public IMessage.Type MsgType => IMessage.Type.Face;

        public MessageBase ToMessage() => new FaceMessage()
        {
            FaceId = ID.ToString(),
        };

        public string ToPlainMessage() => "";
    }
}
