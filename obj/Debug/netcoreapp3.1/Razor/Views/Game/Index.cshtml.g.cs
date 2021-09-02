#pragma checksum "D:\Project\PokerGame\Views\Game\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "65ce971f67f4e69c6f7ac7e54b20b2544e03921d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Game_Index), @"mvc.1.0.view", @"/Views/Game/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Project\PokerGame\Views\_ViewImports.cshtml"
using PokerGame;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Project\PokerGame\Views\_ViewImports.cshtml"
using PokerGame.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"65ce971f67f4e69c6f7ac7e54b20b2544e03921d", @"/Views/Game/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2d0a366afe30219dcda6f6d8bbbbc50583d6300f", @"/Views/_ViewImports.cshtml")]
    public class Views_Game_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\Project\PokerGame\Views\Game\Index.cshtml"
  
    ViewData["Title"] = "Game";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div class=""alert alert-success alert-dismissible fade show"">
    <strong>恭喜你，你赢了!</strong>
</div>
<div class=""alert alert-warning alert-dismissible fade show"">
    <strong>运气不好，你输了</strong>
</div>
<div class=""alert alert-info alert-dismissible fade show"">
    <strong>真巧，打平了</strong>
</div>
<div class=""row"">

    <div id=""room-area"" class=""col-sm-12"">
        <div class=""card"">
            <div class=""card-header"">准备游戏</div>
            <div class=""card-body"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "65ce971f67f4e69c6f7ac7e54b20b2544e03921d3787", async() => {
                WriteLiteral(@"
                    <div class=""form-group"">
                        <label for=""name"">匹配房间</label>
                        <input id=""room-no"" type=""number"" class=""form-control"" placeholder=""请输入4位数字房间号"" maxlength=""4"">
                    </div>
                    <button id=""btn-room"" type=""button"" class=""btn btn-primary"">确定</button>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n    <div id=\"game-area\" class=\"col-sm-12 hide\">\r\n        <div class=\"card bg-info text-white\">\r\n            <div class=\"card-header\">房间号-</div>\r\n            <div class=\"card-body\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "65ce971f67f4e69c6f7ac7e54b20b2544e03921d5670", async() => {
                WriteLiteral(@"
                    <div class=""form-group"">
                        <label for=""name"">抽取数量</label>
                        <input id=""setp"" type=""number"" class=""form-control"" placeholder=""输入1位正整数"" maxlength=""1"">
                    </div>
                    <button id=""btn-step"" type=""button"" class=""btn btn-primary"">确定</button>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            </div>
        </div>
    </div>

    <div id=""message-area"" class=""col-sm-12 pt-3"">
        <div class=""card"">
            <div class=""card-header"">消息</div>
            <div class=""card-body"">
                <textarea class=""form-control"" id=""message"" rows=""5""></textarea>
            </div>
        </div>
    </div>
</div>

");
            DefineSection("scripts", async() => {
                WriteLiteral(@"

    <script>
        var ws;
        //是否需要重新连接
        var lockReconnect = false;
        var wsUrl;
        var showWait = false;

        //初始化事件
        window.onload = function () {
            var scheme = document.location.protocol == ""https:"" ? ""wss"" : ""ws"";
            var port = document.location.port ? ("":"" + document.location.port) : """";
            var pathname = document.location.pathname == ""/"" ? """" : document.location.pathname;
            wsUrl = scheme + ""://"" + document.location.hostname + pathname + port + ""/ws"" ;
            CreateWebSocket(wsUrl);
            $(""#game-area"").hide();
            $("".alert"").hide();
        }

        //创建房间事件
        $(""#btn-room"").on(""click"", function () {
            $(this).attr(""disabled"", ""disabled"");
            var roomNo = $(""#room-no"").val();
            if (!roomNo) {
                alert(""请输入房间号"");
                $(this).removeAttr(""disabled"");
                return;
            } else if (!/^\d{4}$/.test(roomNo");
                WriteLiteral(@")) {
                alert(""请输入4位数字房间号"");
                $(this).removeAttr(""disabled"");
                return;
            }

            ws.send(clientMsg(""inroom"", """", roomNo));
        });

        //抽取事件
        $(""#btn-step"").on(""click"", function () {
            $(this).attr(""disabled"", ""disabled"");
            var step = $(""#setp"").val();
            if (!step) {
                alert(""请输入数量"");
                $(this).removeAttr(""disabled"");
                return;
            } else if (!/^\d{1}$/.test(step)) {
                alert(""请输入1位数字"");
                $(this).removeAttr(""disabled"");
                return;
            }

            ws.send(clientMsg(""step"", """", step));
        })


        //创建websocket
        function CreateWebSocket(url) {
            try {
                wsUrl = url;
                ws = new WebSocket(url);
                initEventHandle();
            } catch (e) {
                Reconnect(url);
            }
        }


    ");
                WriteLiteral(@"    //封装websocket的接口函数
        function initEventHandle() {
            ws.onclose = function () {
                showMessage(""连接关闭"");
                Reconnect(wsUrl);
            }

            ws.onerror = function () {
                showMessage(""传输异常"");
                Reconnect(wsUrl);
            }

            ws.onopen = function () {
                showMessage(""连接成功..."");
                //心跳检测
                heartCheck.reset().start();
            }

            //接收到服务端消息
            ws.onmessage = function (event) {
                //showMessage(event.data);
                var serverMessage = JSON.parse(event.data);
                if (serverMessage) {

                    //执行响应方法
                    if (action[serverMessage.type]) {
                        action[serverMessage.type].call(this, serverMessage);
                    }

                    //消息
                    if (serverMessage.message) {
                        showMessage(serverMessage.messa");
                WriteLiteral(@"ge)
                    }
                }

            }
        }

        //响应方法
        var action = {
            inroom: function (serverMessage) {//创建房间
                //-2.房间已满 -1.房间号错误  1.创建房间 2.第二个玩家
                showWait = false;
                if (serverMessage.code == 2) {
                    action.readyGame(serverMessage.data);
                } else if (serverMessage.code == 1) {
                    showWait = true;
                } else {
                    $(""#btn-room"").removeAttr(""disabled"");
                }
            },
            step: function (serverMessage) {//抽取事件
                showWait = false;
                if (serverMessage.code == -3) {
                    //频繁输入 不处理
                } else if (serverMessage.code == -2) {
                    //找不到用户 重新开始
                    action.endGame();
                } else if (serverMessage.code == -1) {
                    //数量有误 重新输入
                    $(""#btn-step"").removeAttr(""disabled"");
                WriteLiteral(@"
                } else if (serverMessage.code == 1) {
                    //等待对方输入
                    showWait = true;
                } else if (serverMessage.code == 2) {
                    //都大于0
                    $(""#btn-step"").removeAttr(""disabled"");
                } else if (serverMessage.code == 3) {
                    //赢了0 重新开始
                    action.endGame();
                    $("".alert"").hide();
                    $("".alert-success"").fadeIn();
                } else if (serverMessage.code == 4) {
                    //输了 重新开始
                    action.endGame();
                    $("".alert"").hide();
                    $("".alert-warning"").fadeIn();
                } else if (serverMessage.code == 5) {
                    //平了 重新开始
                    action.endGame();
                    $("".alert"").hide();
                    $("".alert-info"").fadeIn();
                }
            },
            offline: function () {
                //离线 重新开始
          ");
                WriteLiteral(@"      action.endGame();
            },
            heartbeat: function (serverMessage) {//心跳检测
                if (showWait) {//显示等待 第一行加.
                    funShowWait();
                }
                heartCheck.reset().start();
            },
            readyGame: function (roomNo) {
                $("".alert"").hide();
                $(""#game-area .card-header"").html(""房间号-"" + roomNo);
                $(""#btn-step"").removeAttr(""disabled"");
                $(""#room-area"").hide();
                $(""#game-area"").show();
            },
            endGame: function () {
                $(""#btn-room"").removeAttr(""disabled"");
                $(""#room-area"").show();
                $(""#game-area"").hide();
            }
        }

        //重连
        function Reconnect(url) {
            if (lockReconnect) return;
            lockReconnect = true;
            //没连接上会继续连接，设置延迟避免请求过多
            setTimeout(function () {
                showMessage(""尝试重新连接..."" + new Date().format(""");
                WriteLiteral(@"yyyy-MM-dd hh:mm:ss""));
                CreateWebSocket(url);
                lockReconnect = false;
            }, 5000);
        }

        //js格式化日期,调用的时候直接new Date().format(""yyyy-MM-dd hh:mm:ss"")
        Date.prototype.format = function (fmt) {
            var o = {
                ""M+"": this.getMonth() + 1,//月份
                ""d+"": this.getDate(),//日
                ""h+"": this.getHours(),//小时
                ""m+"": this.getMinutes(),//分钟
                ""s+"": this.getSeconds(),//秒
                ""q+"": Math.floor((this.getMonth() + 3 / 3)),//季度
                ""S+"": this.getMilliseconds()//毫秒
            }
            if (/(y+)/.test(fmt)) {
                fmt = fmt.replace(RegExp.$1, (this.getFullYear() + """").substr(4 - RegExp.$1.length));
            }
            for (var k in o) {
                if (new RegExp(""("" + k + "")"").test(fmt)) {
                    fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (""00"" + o[k]).substr(("""" + o[k]).length));
             ");
                WriteLiteral(@"   }
            }
            return fmt;
        }

        //心跳设置
        var heartCheck = {
            timeout: 5000,
            timeoutObj: null,
            serverTimeoutObj: null,
            reset: function () {
                clearTimeout(this.timeoutObj);
                clearTimeout(this.serverTimeoutObj);
                return this;
            },
            start: function () {
                var self = this;
                this.timeoutObj = setTimeout(function () {
                    //发送一条心跳，后台接收发哦后返回一个心跳消息
                    //onmessage拿到消息就说明连接正常
                    ws.send(clientMsg(""heartbeat"", """", { date: new Date().format(""yyyy-MM-dd hh:mm:ss"") }));
                    //console.log(""Client request heartbeat:"" + new Date().format(""yyyy-MM-dd hh:mm:ss""));
                    self.serverTimeoutObj = setTimeout(function () {
                        //如果超过一定时间还没重置，说明后端主动断开了
                        ws.close();
                        //如果onclose会执行reconnect，我们");
                WriteLiteral(@"执行ws.close()就行了
                        //如果直接执行reconnect会触发onclose导致重连两次
                    }, self.timeout)
                }, this.timeout)
            }
        }

        //显示消息 显示5条消息
        function showMessage(msg) {
            var textMsg = $(""#message"").val();
            if (textMsg.split(""\n"").length >= 5) {
                textMsg = textMsg.split(""\n"").slice(0, 4).join(""\n"");
            }
            $(""#message"").val(msg + '\n' + textMsg);
        }

        //显示等待 第一行加.
        function funShowWait() {
            var textMsg = $(""#message"").val();
            textMsg = textMsg.split(""\n"")[0] + "".\n"" + textMsg.split(""\n"").slice(1, 4).join(""\n"");
            $(""#message"").val(textMsg);
        }

        //获取客户端字符串
        function clientMsg(type, message, data) {
            return JSON.stringify({ ""type"": type, ""message"": message, ""data"": data });
        }

    </script>
");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591