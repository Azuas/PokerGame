using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace PokerGame.Models
{
    public class Player
    {
        /// <summary>
        /// websocket
        /// </summary>
        public WebSocket WebSocket { get; set; }

        /// <summary>
        /// 房间号
        /// </summary>
        public string RoomNo { get; set; }

        /// <summary>
        /// 原始数量
        /// </summary>
        public int OriginalCount { get; set; }

        /// <summary>
        /// 当前数量
        /// </summary>
        public int CurrentCount { get; set; }

        /// <summary>
        /// 回合
        /// </summary>
        public int Round { get; set; }

        /// <summary>
        /// 步进
        /// </summary>
        public int Step { get; set; }
    }
}
