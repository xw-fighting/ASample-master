using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ASample.SignalRNet.WebSite.Core
{
    [HubName("chat")]
    public class ChatHub:Hub
    {
        public static ConcurrentDictionary<string, string> OnLineUsers = new ConcurrentDictionary<string, string>();

        [HubMethodName("send")]
        public void Send(string message)
        {
            message = HttpUtility.HtmlEncode(message).Replace("\r\n", "<br/>").Replace("\n", "<br/>");
            Clients.All.receiveMessage(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message);
        }

        [HubMethodName("sendOne")]
        public void Send(string clientName, string message)
        {
            message = HttpUtility.HtmlEncode(message).Replace("\r\n", "<br/>").Replace("\n", "<br/>");
            Clients.Caller.receiveMessage(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), string.Format("您对 {0}", clientName), message);
            Clients.Client(clientName).receiveMessage(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), string.Format("{0} 对您", clientName), message);
        }

        public override Task OnConnected()
        {
            string clientName = Context.QueryString["clientName"].ToString();
            OnLineUsers.AddOrUpdate(Context.ConnectionId, clientName, (key, value) => clientName);
            Clients.All.userChange(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), string.Format("{0} 加入了。", clientName), OnLineUsers.ToArray());
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string clientName = Context.QueryString["clientName"].ToString();
            Clients.All.userChange(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), string.Format("{0} 离开了。", clientName), OnLineUsers.ToArray());
            OnLineUsers.TryRemove(Context.ConnectionId, out clientName);
            return base.OnDisconnected(stopCalled);
        }
    }
}