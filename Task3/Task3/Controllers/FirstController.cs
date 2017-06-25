using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;

namespace Task3.Controllers
{
    public class FirstController : ApiController
    {
        public static WebSocket firstsocket;
        public HttpResponseMessage Get()
        {
            //HttpResponseMessage res;
            //res = new HttpResponseMessage();
            //res.Content = new StringContent("Hello");
            //return res;
            if (HttpContext.Current.IsWebSocketRequest)
                HttpContext.Current.
                AcceptWebSocketRequest(ProcessSocket);
            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        }
        private async Task ProcessSocket(AspNetWebSocketContext context)
        {
            firstsocket = context.WebSocket;
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
                    if (Second.secondsocket != null)
                        await Second.secondsocket.SendAsync(
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
    }
}
