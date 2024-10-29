using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyBot.Message;

namespace QQAPI.LLOneBot.Message
{
    public interface IMessage
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public enum Type
        {
            Plain,
            At,
            AtAll,
            Quote,
            Face,
            Image,
            Voice,
            Json,
            Other
        }

        Type MsgType { get; }
        string ToPlainMessage();
        MessageBase ToMessage();
    }
}
