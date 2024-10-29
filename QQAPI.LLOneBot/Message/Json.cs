

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyBot.Message;

namespace QQAPI.LLOneBot.Message
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
            json = msg.Data.Data;
        }
        public IMessage.Type MsgType => IMessage.Type.Json;

        public MessageBase ToMessage() => new JsonMessage(json);

        public string ToPlainMessage() => "";
    }
}
