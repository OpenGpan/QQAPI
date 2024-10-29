
namespace QQAPI.Message
{
    public class At : IMessage
    {
        public long qq;
        public At(long qq)
        {
            this.qq = qq;
        }
   
        public IMessage.Type MsgType => IMessage.Type.At;

        public string ToPlainMessage() => "";
    }
}
