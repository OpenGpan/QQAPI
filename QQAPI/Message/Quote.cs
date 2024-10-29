
namespace QQAPI.Message
{
    public class Quote : IMessage
    {
        public int id;
        public long groupid;
        public Quote(int id, long groupid)
        {
            this.id = id;
            this.groupid = groupid;
        }
  
        public IMessage.Type MsgType => IMessage.Type.Quote;

        public string ToPlainMessage() => "";

    }
}
