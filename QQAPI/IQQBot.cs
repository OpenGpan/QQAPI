using QQAPI.Message;
using QQAPI.Reply;
using static QQAPI.Handle;

namespace QQAPI
{
    public abstract class IQQBot
    {
        public IQQBot(string address, long qq, string key)
        {
          
        }
        public abstract Task StartAsync();
        public abstract void LoadHandle();
        public HandleMessage? GroupMessage;
        public HandleMessage? FriendMessage;
        public HandleMessage? TempMessage;
        public HandleInvitedJoinGroup? InvitedJoinGroup;
        public HandleNewFriendApply? NewFriendApply;
        public HandleBotMuted? BotMuted;
        public HandleBotUnMuted? BotUnMuted;
        public HandleBotKick? BotKick;
        public abstract List<Group> Groups();
    }
    public static class Handle
    {
        public delegate void HandleMessage(Messages msg);
        public delegate bool HandleInvitedJoinGroup(long groupid, long userid, string message);
        public delegate bool HandleNewFriendApply(long userid, string message);
        public delegate void HandleBotMuted(long groupid, long user, int mutedtime);
        public delegate void HandleBotUnMuted(long groupid, long user);
        public delegate void HandleBotKick(long groupid);
    }
}