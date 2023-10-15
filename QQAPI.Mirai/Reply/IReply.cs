using QQAPI.MiraiNET.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQAPI.MiraiNET.Reply
{
    public interface IReply
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public enum ReplyType
        {
            Temp,
            Friend,
            Group
        }
        public ReplyType Type { get; }
        public Task MessageSend(Messages messages);
        public Task MessageReplay(Messages messages);
    }
}
