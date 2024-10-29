using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQAPI.Message
{
    public class Plain : IMessage
    {
        public string message;
        public Plain(string message)
        {
            this.message = message;
        }


        public IMessage.Type MsgType => IMessage.Type.Plain;

        public string ToPlainMessage() => message;
    }
}
