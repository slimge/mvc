using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace Task3
{
    /// <summary>
    /// Summary description for Second
    /// </summary>
    public class Second : IHttpHandler
    {
        public static WebSocket secondsocket;
        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
                context.AcceptWebSocketRequest(ProcessSocket);
        }

        public async Task ProcessSocket(AspNetWebSocketContext context)
        {
            secondsocket = context.WebSocket;
            ArraySegment<byte> buf =
                new ArraySegment<byte>(new byte[10000]);
            while (true)
            {
                WebSocketReceiveResult res =
                    await context.WebSocket.ReceiveAsync(buf,
                    CancellationToken.None);
                if (context.WebSocket.State
                    == WebSocketState.Open)
                {
                    String txt = "[" +
                    Encoding.UTF8.GetString(buf.Array, 0, res.Count)
                    + "]";
                    if (Controllers.FirstController.firstsocket != null)
                        await Controllers.FirstController.firstsocket.SendAsync(
                        new ArraySegment<byte>(Encoding.UTF8.GetBytes(txt)),
                        WebSocketMessageType.Text, true, CancellationToken.None);
                    else
                        await context.WebSocket.SendAsync(
                            new ArraySegment<byte>(Encoding.UTF8.GetBytes(txt)),
                            WebSocketMessageType.Text, true, CancellationToken.None);

                }
                else
                    break;
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}