﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyBot.Message;

namespace QQAPI.LLOneBot.Message
{
    public class Voice : IMessage
    {
        /// <summary>
        /// 网络音频/接收音频
        /// </summary>
        public string Url = "";
        /// <summary>
        /// 本地音频,用于发送
        /// </summary>
        public string Path = "";
        /// <summary>
        /// 创建声音类
        /// </summary>
        /// <param name="image">根据http自动判断是网络图片还是本地</param>
        public Voice(string voice)
        {
            if (voice.StartsWith("http"))
                Url = voice;
            else
                Path = voice;
        }
        public Voice(RecordMessage msg)
        {
            Url = msg.Data.Url;
            Path = msg.Data.File;
        }
        public IMessage.Type MsgType => IMessage.Type.Voice;

        public MessageBase ToMessage() => new RecordMessage(Path, "", Url);

        public string ToPlainMessage() => "";
    }
}