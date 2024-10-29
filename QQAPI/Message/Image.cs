﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQAPI.Message
{
    public class Image : IMessage
    {
        /// <summary>
        /// 网络图片/接收图片
        /// </summary>
        public string Url = "";
        /// <summary>
        /// 本地图片,用于发送
        /// </summary>
        public string Path = "";
        /// <summary>
        /// 创建图片类
        /// </summary>
        /// <param name="image">根据http自动判断是网络图片还是本地</param>
        public Image(string image)
        {
            if (image.StartsWith("http"))
                Url = image;
            else
                Path = image;
        }
      
        public IMessage.Type MsgType => IMessage.Type.Image;

        public string ToPlainMessage() => "";
    }
}
