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
  
        public IMessage.Type MsgType => IMessage.Type.Face;

        public string ToPlainMessage() => "";
    }
}
