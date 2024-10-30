using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

using QQAPI.LLOneBot.Message;
using QQAPI.LLOneBot.Reply;
using UnifyBot;
using UnifyBot.Receiver;
using UnifyBot.Receiver.EventReceiver.Notice;
using UnifyBot.Receiver.EventReceiver.Request;
using UnifyBot.Receiver.MessageReceiver;
using static QQAPI.LLOneBot.Handle;

namespace QQAPI.LLOneBot
{
    public class QQBot
    {
        public long BotQQ;
        readonly Bot bot;
        public QQBot(long botqq, string address, int wsport, int httpport, string key = "")
        {
            BotQQ = botqq;
            bot = new Bot(new UnifyBot.Model.Connect(address, wsport, httpport, true, key));
        }
        public async Task StartAsync()
        {
            await bot.StartAsync();
        }
        public void LoadHandle()
        {
            bot.MessageReceived.OfType<MessageReceiver>().Subscribe(x =>
            {
                if (x.BotQQ == BotQQ)
                    //只能接收到消息（所有类型）
                    switch (x.MessageType)
                    {
                        case UnifyBot.Model.MessageType.Private:
                            FriendMessage?.Invoke(new Messages((PrivateReceiver)x));
                            break;
                        case UnifyBot.Model.MessageType.Group:
                            GroupMessage?.Invoke(new Messages((GroupReceiver)x));
                            break;
                        case UnifyBot.Model.MessageType.Unknown:
                            TempMessage?.Invoke(new Messages(x, this));
                            break;
                    }
            });

            bot.EventReceived
            .OfType<RequestGroup>()
            .Subscribe(async x =>
            {
                if (x.BotQQ == BotQQ)
                    if (InvitedJoinGroup?.Invoke(x.GroupQQ, Convert.ToInt64(x.QQ), x.Common) == true)
                        await x.Agree();
            });
            bot.EventReceived
            .OfType<RequestFriend>()
            .Subscribe(async x =>
            {
                if (x.BotQQ == BotQQ)
                    if (NewFriendApply?.Invoke(Convert.ToInt64(x.QQ), x.Common) == true)
                        await x.Agree();
            });
            bot.EventReceived
           .OfType<GroupBan>()
           .Subscribe(x =>
           {
               if (x.QQ == BotQQ)
                   switch (x.NoticeSubType)
                   {
                       case UnifyBot.Model.NoticeSubType.Ban:
                           BotMuted?.Invoke(x.GroupQQ, x.OperatorQQ, (int)x.Duration);
                           break;
                       case UnifyBot.Model.NoticeSubType.LeftBan:
                           BotUnMuted?.Invoke(x.GroupQQ, x.OperatorQQ);
                           break;
                   }

           });
            bot.EventReceived
          .OfType<GroupMemberDecrease>()
          .Subscribe(x =>
          {
              if (x.BotQQ == BotQQ)
                  if (x.NoticeSubType == UnifyBot.Model.LeaveSubType.Kick && x.QQ == BotQQ)
                  {
                      BotKick?.Invoke(x.GroupQQ);
                  }
          });
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
            foreach (var v in bot.Groups)
            {
                gps.Add(new Group(v, BotQQ));
            }
            return gps;
        }
        public Bot GetBot() => bot;
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