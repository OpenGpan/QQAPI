using QQAPI.Message;
using static QQAPI.Reply.IReply;

namespace QQAPI.Reply
{
    public abstract class Temp : IReply
    {
        public long ID { get; set; } = 0;
        public long GroupID = 0;
        public string Name { get; set; } = string.Empty;
        public Temp(long id, long groupiD)
        {

        }

        public ReplyType Type => ReplyType.Group;
        public abstract Task MessageSend(Messages messages);
        public abstract Task MessageReplay(Messages messages);
    }
}
