﻿using QQAPI.LLOneBot.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQAPI.LLOneBot.Reply
{
    public interface IReply
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public enum MessagesType
        {
            Temp,
            Friend,
            Group
        }
        public MessagesType Type { get; }
        public Task MessageSend(Messages messages);
        public Task MessageReplay(Messages messages);
    }
}
