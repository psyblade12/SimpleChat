using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Linq;
using System.Collections.Generic;

namespace SignalRChat
{
    public class ChatHub : Hub
    {
        static Dictionary<string, string> onlinePeople = new Dictionary<string, string>();
        public void Join(string name)
        {
            onlinePeople.Add(Context.ConnectionId, name);
            Clients.All.broadcastmessage("System", name+" đã vào phòng chát");
        }
        public void Send(string name, string message)
        {
            Clients.All.broadcastMessage(name, message);
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            Clients.All.broadcastmessage("System", onlinePeople[Context.ConnectionId] + " đã rời khỏi phòng chát");
            onlinePeople.Remove(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
    }
}