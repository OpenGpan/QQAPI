using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mirai.Net.Data.Events.Concretes.Group;
using Mirai.Net.Data.Events.Concretes.Request;
using Mirai.Net.Data.Messages.Receivers;
using Mirai.Net.Sessions;
using Mirai.Net.Sessions.Http.Managers;
using Mirai.Net.Utils.Scaffolds;
using QQAPI.MiraiNET.Message;
using QQAPI.MiraiNET.Reply;
using static QQAPI.MiraiNET.Handle;

namespace QQAPI.MiraiNET
{
    public class QQBot
    {
        readonly MiraiBot bot;
        public QQBot(string address, long qq, string key)
        {
            bot = new MiraiBot
            {
                Address = address,
                QQ = qq.ToString(),
                VerifyKey = key
            };
        }
        public async Task StartAsync()
        {
            await bot.LaunchAsync();
        }
        public void LoadHandle()
        {
            bot.MessageReceived
            .OfType<GroupMessageReceiver>()
            .Subscribe(x => { GroupMessage?.Invoke(new Messages(x)); });
            bot.MessageReceived
           .OfType<FriendMessageReceiver>()
           .Subscribe(x => FriendMessage?.Invoke(new Messages(x)));
            bot.MessageReceived
           .OfType<TempMessageReceiver>()
           .Subscribe(x => TempMessage?.Invoke(new Messages(x)));

            bot.EventReceived
            .OfType<NewInvitationRequestedEvent>()
            .Subscribe(async x =>
            {
                if (InvitedJoinGroup?.Invoke(Convert.ToInt64(x.GroupId), Convert.ToInt64(x.FromId), x.Message) == true)
                    await RequestManager.HandleNewInvitationRequestedAsync(x, Mirai.Net.Data.Shared.NewInvitationRequestHandlers.Approve, "");
            });
            bot.EventReceived
            .OfType<NewFriendRequestedEvent>()
            .Subscribe(async x =>
            {
                if (NewFriendApply?.Invoke(Convert.ToInt64(x.FromId), x.Message) == true)
                    await RequestManager.HandleNewFriendRequestedAsync(x, Mirai.Net.Data.Shared.NewFriendRequestHandlers.Approve, "");
            });
            bot.EventReceived
           .OfType<MutedEvent>()
           .Subscribe(x => BotMuted?.Invoke(Convert.ToInt64(x.Operator.Group), Convert.ToInt64(x.Operator.Id), Convert.ToInt32(x.Period)));
            bot.EventReceived
           .OfType<UnmutedEvent>()
           .Subscribe(x => BotUnMuted?.Invoke(Convert.ToInt64(x.Operator.Group), Convert.ToInt64(x.Operator.Id)));
            bot.EventReceived
          .OfType<KickedEvent>()
          .Subscribe(x => BotKick?.Invoke(Convert.ToInt64(x.Group)));
            Console.WriteLine("Loaded");
        }
        public HandleMessage? GroupMessage;
        public HandleMessage? FriendMessage;
        public HandleMessage? TempMessage;
        public HandleInvitedJoinGroup? InvitedJoinGroup;
        public HandleNewFriendApply? NewFriendApply;
        public HandleBotMuted? BotMuted;
        public HandleBotUnMuted? BotUnMuted;
        public HandleBotKick? BotKick;
        public List<Group> Groups()
        {
            List<Group> gps = new List<Group>();
            foreach (Mirai.Net.Data.Shared.Group? v in AccountManager.GetGroupsAsync().GetAwaiter().GetResult())
            {
                gps.Add(new Group(v));
            }
            return gps;
        }
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