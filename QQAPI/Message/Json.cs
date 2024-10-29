using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQAPI.Message
{
    public class Json : IMessage
    {
        public string json;
        public Json(string json)
        {
            this.json = json;
        }
        public IMessage.Type MsgType => IMessage.Type.Json;

        public string ToPlainMessage() => "";
    }
}
