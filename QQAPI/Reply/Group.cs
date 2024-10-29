using QQAPI.Message;
using static QQAPI.Reply.IReply;

namespace QQAPI.Reply
{
    public abstract class Group : IReply
    {
        public long ID { get; set; }
        public long SenderID;
        public string Name { get; set; }
        public bool IsAdmin = false;
        public Group(long id)
        {
            ID = id;
            Name = "";
        }

        public ReplyType Type => ReplyType.Group;

        public abstract Task MessageSend(Messages messages);
        public abstract Task MessageReplay(Messages messages);

    }
}
