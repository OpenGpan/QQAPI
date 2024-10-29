using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQAPI.Message
{
    public abstract class Other : IMessage
    {

        public IMessage.Type MsgType => IMessage.Type.Other;

        public string ToPlainMessage() => "";
    }
}
