using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebVueTest.Controllers;

namespace WebVueTest.Models
{
    public class CommonHub : Hub
    {
        public async Task AddGroup(string group)
        {
            this.Groups.AddToGroupAsync(Context.ConnectionId, group);            
        }

        public async Task SaveComment(MergeUserComment comment, string groupName)
        {
            var group = this.Clients.Group(groupName);
            await this.Clients.GroupExcept(groupName, Context.ConnectionId).SendAsync("SaveComment", comment);
        }

        public async Task ChangeModel(string groupName)
        {
            var group = this.Clients.Group(groupName);
            await this.Clients.GroupExcept(groupName, Context.ConnectionId).SendAsync("ChangeModel");
        }

        public async Task SendText(string message)
        {
            await this.Clients.All.SendAsync("Send", message);
        }

    }
}
