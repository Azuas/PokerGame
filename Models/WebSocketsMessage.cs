using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerGame.Models
{
    /// <summary>
    /// 客户端消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClientMessage<T>
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public T Data { get; set; }
    }

    /// <summary>
    /// 服务端消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceMessage<T>
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
