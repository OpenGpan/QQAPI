﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyBot.Message;

namespace QQAPI.LLOneBot.Message
{
    public class Other : IMessage
    {
        MessageBase other;
        public Other(MessageBase other)
        {
            this.other = other;
        }
        public IMessage.Type MsgType => IMessage.Type.Other;

        public MessageBase ToMessage() => other;

        public string ToPlainMessage() => "";
    }
}
