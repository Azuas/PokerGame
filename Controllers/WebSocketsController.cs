using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PokerGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace PokerGame.Controllers
{
    public class WebSocketsController : Controller
    {
        private readonly ILogger<WebSocketsController> _logger;

        /// <summary>
        /// 玩家列表
        /// </summary>
        private static List<Player> PlayerList = new List<Player>();

        private static int[] PokerList = new int[] { 3, 5, 7 };

        public WebSocketsController(ILogger<WebSocketsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// WebSocket入口
        /// </summary>
        /// <returns></returns>
        [HttpGet("/ws")]
        public async Task Get()
        {
            try
            {
                if (HttpContext.WebSockets.IsWebSocketRequest)
                {
                    using WebSocket webSocket = await
                                       HttpContext.WebSockets.AcceptWebSocketAsync();
                    await Echo(webSocket);
                }
                else
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 单个循环
        /// </summary>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            _logger.Log(LogLevel.Information, "Message received from Client");
            ClientMessage<dynamic> clientMessage = null;
            var strResult = string.Empty;

            while (!result.CloseStatus.HasValue)
            {
                clientMessage = null;
                try
                {
                    strResult = Encoding.UTF8.GetString(buffer).TrimEnd().TrimEnd('\0');
                    clientMessage = JsonConvert.DeserializeObject<ClientMessage<dynamic>>(strResult);
                }
                catch (Exception ex)
                {
                }
                if (clientMessage != null)
                {
                    switch (clientMessage.Type)
                    {
                        case "inroom"://创建或加入房间
                            await InRoomAsync(webSocket, strResult);
                            break;

                        case "step"://抽取事件
                            await Step(webSocket, strResult);
                            break;

                        case "heartbeat"://心跳包
                            await HeartbeatAsync(webSocket);
                            break;
                        default:
                            break;
                    }
                }

                buffer = new byte[1024 * 4];
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                _logger.Log(LogLevel.Information, "Message received from Client");

            }
            //离线
            var player = PlayerList.FirstOrDefault(f => f.WebSocket == webSocket);
            if (player != null)
            {
                PlayerList.Remove(player);
                //通知玩家2
                var roomPlayers = PlayerList.Where(w => w.RoomNo == player.RoomNo).ToList();
                if (roomPlayers != null && roomPlayers.Count > 0)
                {
                    foreach (var item in roomPlayers)
                    {
                        var serviceMessage = new ServiceMessage<string>()
                        {
                            Type = "offline",
                            Message = "对方玩家已离线,请重新匹配房间",
                            Code = -1,   //-1.对方玩家已离线,
                            Data = player.RoomNo
                        };
                        await SocketSendAsync(item.WebSocket, serviceMessage);
                    }
                }
            }

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            _logger.Log(LogLevel.Information, "WebSocket connection closed");
        }

        /// <summary>
        /// 创建或进入房间
        /// </summary>
        /// <param name="webSocket"></param>
        /// <param name="strResult"></param>
        /// <returns></returns>
        private async Task InRoomAsync(WebSocket webSocket, string strResult)
        {
            try
            {
                var objResult = JsonConvert.DeserializeObject<ClientMessage<string>>(strResult);
                var roomNo = objResult.Data;
                var serviceMessage = new ServiceMessage<string>()
                {
                    Type = "inroom",
                    Code = 0    //-2.房间已满 -1.房间号错误  1.创建房间 2.第二个玩家
                };

                //房间号有误
                if (string.IsNullOrEmpty(roomNo) || !new Regex(@"^\d{4}$").IsMatch(roomNo))
                {
                    serviceMessage.Code = -1;
                    serviceMessage.Message = "房间号有误，请重新输入";
                    await SocketSendAsync(webSocket, serviceMessage);
                    return;
                }

                //清除之前旧玩家
                var oldPlayers = PlayerList.Where(w => w.WebSocket == webSocket).ToList(); ;
                if (oldPlayers != null && oldPlayers.Count > 0)
                {
                    for (int i = 0; i < oldPlayers.Count; i++)
                    {
                        PlayerList.Remove(oldPlayers[i]);
                        //通知玩家2
                        var otherPlayers = PlayerList.Where(w => w.RoomNo == oldPlayers[i].RoomNo).ToList();
                        if (otherPlayers != null && otherPlayers.Count > 0)
                        {
                            foreach (var item in otherPlayers)
                            {
                                var offlineMessage = new ServiceMessage<string>()
                                {
                                    Type = "offline",
                                    Message = "对方玩家已离线,请重新匹配房间",
                                    Code = -1,   //-1.对方玩家已离线,
                                    Data = oldPlayers[i].RoomNo
                                };
                                await SocketSendAsync(item.WebSocket, offlineMessage);
                            }
                        }
                    }
                }

                ServiceMessage<string> otherMessage = null;
                Player otherPlayer = null;

                lock (this)
                {
                    var roomPlayers = PlayerList.Where(w => w.RoomNo == roomNo).ToList();

                    //超过两人
                    if (roomPlayers != null && roomPlayers.Count >= 2)
                    {
                        serviceMessage.Code = -2;
                        serviceMessage.Message = "该房间已满，请重新输入";
                        
                    }

                    //创建房间
                    if (roomPlayers.Count <= 0)
                    {
                        var pokerCount = PokerList[GetRandom(0, 3)];
                        PlayerList.Add(new Player()
                        {
                            WebSocket = webSocket,
                            RoomNo = roomNo,
                            Round = 0,
                            OriginalCount = pokerCount,
                            CurrentCount = pokerCount
                        });

                        serviceMessage.Code = 1;
                        serviceMessage.Message = "创建房间，等待第二个玩家";
                    }

                    //第二个玩家
                    if (roomPlayers.Count >= 1)
                    {
                        otherPlayer = roomPlayers.FirstOrDefault(f => f.RoomNo == roomNo);
                        otherMessage = new ServiceMessage<string>()
                        {
                            Type = "inroom",
                            Message = "人齐了，游戏开始，请输入抽取数量并点击确定",
                            Data = roomNo,
                            Code = 2
                        };

                        serviceMessage.Code = 2;
                        serviceMessage.Data = roomNo;
                        serviceMessage.Message = "人齐了，游戏开始，请输入抽取数量并点击确定";

                        var pokerCount = PokerList[GetRandom(0, 2)];
                        PlayerList.Add(new Player()
                        {
                            WebSocket = webSocket,
                            RoomNo = roomNo,
                            OriginalCount = pokerCount,
                            CurrentCount = pokerCount
                        });
                    }
                }

                //通知本人
                await SocketSendAsync(webSocket, serviceMessage);

                //通知另一位玩家
                if (otherPlayer != null && otherMessage != null)
                {
                    await SocketSendAsync(otherPlayer.WebSocket, otherMessage);
                }

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }
        }

        /// <summary>
        /// 抽取事件
        /// </summary>
        /// <param name="webSocket"></param>
        /// <param name="strResult"></param>
        /// <returns></returns>
        private async Task Step(WebSocket webSocket, string strResult)
        {
            try
            {
                var objResult = JsonConvert.DeserializeObject<ClientMessage<string>>(strResult);
                var step = objResult.Data;
                var serviceMessage = new ServiceMessage<string>()
                {
                    Type = "step",
                    Code = 0    //-3.频繁输入 -2.找不到用户 -1.数量有误 1.等待对方输入 2.都大于0 3.赢了 4.输了 5.平了
                };

                //数量有误
                if (string.IsNullOrEmpty(step) || !new Regex(@"^\d{1}$").IsMatch(step))
                {
                    serviceMessage.Code = -1;
                    serviceMessage.Message = "数量有误，请重新输入";
                    await SocketSendAsync(webSocket, serviceMessage);
                    return;
                }

                var player = PlayerList.FirstOrDefault(f => f.WebSocket == webSocket);
                //找不到用户
                if (player == null)
                {
                    serviceMessage.Code = -2;
                    serviceMessage.Message = "数据异常，请重新输入房间";
                    await SocketSendAsync(webSocket, serviceMessage);
                    return;
                }

                var otherPlayer = PlayerList.FirstOrDefault(f => f.RoomNo == player.RoomNo && f.WebSocket != webSocket);
                //对方离线
                if (player == null)
                {
                    serviceMessage.Type = "offline";
                    serviceMessage.Code = -1;
                    serviceMessage.Message = "对方玩家已离线,请重新匹配房间";
                    await SocketSendAsync(webSocket, serviceMessage);
                    return;
                }

                var intStep = Convert.ToInt32(step);

                //对方还没输入
                if (player.Round == otherPlayer.Round)
                {
                    player.Round = player.Round + 1;
                    player.Step = intStep;
                    player.CurrentCount = player.CurrentCount - intStep;

                    serviceMessage.Code = 1;
                    serviceMessage.Message = "等待对方输入";
                    await SocketSendAsync(webSocket, serviceMessage);
                    return;
                }
                else if (player.Round > otherPlayer.Round)
                {
                    //频繁输入
                    serviceMessage.Code = -3;
                    serviceMessage.Message = "请等待对方输入";
                    await SocketSendAsync(webSocket, serviceMessage);
                    return;
                }
                else
                {
                    //双方都已输入
                    player.Round = player.Round + 1;
                    player.Step = intStep;
                    player.CurrentCount = player.CurrentCount - intStep;
                    //都大于0 下一轮
                    if (player.CurrentCount >0 && otherPlayer.CurrentCount > 0)
                    {
                        serviceMessage.Code = 2;
                        serviceMessage.Message = $"第{player.Round}回合结束，请继续";
                        await SocketSendAsync(webSocket, serviceMessage);
                        //通知对手
                        await SocketSendAsync(otherPlayer.WebSocket, serviceMessage);
                        return;
                    }
                    else if (player.CurrentCount > 0 && otherPlayer.CurrentCount <= 0)
                    {
                        //对方输
                        serviceMessage.Code = 3;
                        serviceMessage.Message = $"第{player.Round}回合结束，你赢了！你原来{player.OriginalCount}个，剩余{player.CurrentCount}个；对方原来{otherPlayer.OriginalCount}个，剩余{otherPlayer.CurrentCount}个";
                        await SocketSendAsync(webSocket, serviceMessage);

                        //通知对手
                        serviceMessage.Code = 4;
                        serviceMessage.Message = $"第{otherPlayer.Round}回合结束，你输了！你原来{otherPlayer.OriginalCount}个，剩余{otherPlayer.CurrentCount}个；对方原来{player.OriginalCount}个，剩余{player.CurrentCount}个";
                        await SocketSendAsync(otherPlayer.WebSocket, serviceMessage);
                        PlayerList.Remove(player);
                        PlayerList.Remove(otherPlayer);
                        return;
                    }
                    else if (player.CurrentCount <= 0 && otherPlayer.CurrentCount > 0)
                    {
                        //自己输
                        serviceMessage.Code = 4;
                        serviceMessage.Message = $"第{otherPlayer.Round}回合结束，你输了！你原来{player.OriginalCount}个，剩余{player.CurrentCount}个；对方原来{otherPlayer.OriginalCount}个，剩余{otherPlayer.CurrentCount}个";
                        await SocketSendAsync(webSocket, serviceMessage);

                        //通知对手
                        serviceMessage.Code = 3;
                        serviceMessage.Message = $"第{otherPlayer.Round}回合结束，你赢了！你原来{otherPlayer.OriginalCount}个，剩余{otherPlayer.CurrentCount}个；对方原来{player.OriginalCount}个，剩余{player.CurrentCount}个";
                        await SocketSendAsync(otherPlayer.WebSocket, serviceMessage);
                        PlayerList.Remove(player);
                        PlayerList.Remove(otherPlayer);
                        return;
                    }
                    else
                    {
                        //都小于0
                        serviceMessage.Code = 5;
                        serviceMessage.Message = $"第{player.Round}回合结束，打平！你原来{player.OriginalCount}个，剩余{player.CurrentCount}个；对方原来{otherPlayer.OriginalCount}个，剩余{otherPlayer.CurrentCount}个";
                        await SocketSendAsync(webSocket, serviceMessage);

                        //通知对手
                        serviceMessage.Code = 5;
                        serviceMessage.Message = $"第{otherPlayer.Round}回合结束，打平！你原来{otherPlayer.OriginalCount}个，剩余{otherPlayer.CurrentCount}个；对方原来{player.OriginalCount}个，剩余{player.CurrentCount}个";
                        await SocketSendAsync(otherPlayer.WebSocket, serviceMessage);
                        PlayerList.Remove(player);
                        PlayerList.Remove(otherPlayer);
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }
        }

        /// <summary>
        /// 心跳包
        /// </summary>
        /// <param name="webSocket"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task HeartbeatAsync(WebSocket webSocket)
        {
            var serviceMessage = new ServiceMessage<string>()
            {
                Type = "heartbeat",
                Code = 1,   //1.成功
                Data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            await SocketSendAsync(webSocket, serviceMessage);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="webSocket"></param>
        /// <param name="serviceMessage"></param>
        /// <returns></returns>
        private async Task SocketSendAsync<T>(WebSocket webSocket, ServiceMessage<T> serviceMessage)
        {
            var serverMsg = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(serviceMessage));
            await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        /// <summary>
        /// 获取范围内随机数
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        private int GetRandom(int minValue, int maxValue) {
            Random rd = new Random();
            return rd.Next(minValue, maxValue);
        }
    }
}
