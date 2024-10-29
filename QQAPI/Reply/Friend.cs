using QQAPI.Message;
using static QQAPI.Reply.IReply;

namespace QQAPI.Reply
{
    public abstract class Friend : IReply
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public Friend(long id)
        {
            ID = id;
            Name = "";
        }
       
        public ReplyType Type => ReplyType.Group;
        public abstract Task MessageSend(Messages messages);
        public abstract Task MessageReplay(Messages messages);
    }
}
