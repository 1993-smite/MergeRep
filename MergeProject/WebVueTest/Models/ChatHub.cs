using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebVueTest.Controllers;

namespace WebVueTest.Models
{
    public class ChatHub : Hub
    {
        private string getGroupName(int cardId)
        {
            return $"Merge.Card.{cardId}.Group";
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

        public async Task SendToGroup(MergeUserComment comment)
        {
            await this.Clients.GroupExcept(getGroupName(comment.CardId), Context.ConnectionId).SendAsync("Send", comment);
        }

        public async Task AddGroup(string group)
        {
            this.Groups.AddToGroupAsync(Context.ConnectionId, group);            
        }

        public async Task SendText(string message)
        {
            await this.Clients.All.SendAsync("Send", message);
        }
    }
}
