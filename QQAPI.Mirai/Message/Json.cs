using Mirai.Net.Data.Messages;
using Mirai.Net.Data.Messages.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQAPI.MiraiNET.Message
{
    public class Json : IMessage
    {
        public string json;
        public Json(string json)
        {
            this.json = json;
        }
        public Json(JsonMessage msg)
        {
            json = msg.Json;
        }
        public IMessage.Type MsgType => IMessage.Type.Json;

        public MessageBase ToMessage() => new JsonMessage()
        {
            Json = json,
        };

        public string ToPlainMessage() => "";
    }
}
