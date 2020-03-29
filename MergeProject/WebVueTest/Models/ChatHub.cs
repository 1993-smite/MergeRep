using Microsoft.AspNetCore.Hosting.Internal;
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

        public async Task Send(MergeUserComment comment)
        {
            comment.CreatedUser = new User { Id=comment.CardId };
            await this.Clients.GroupExcept(getGroupName(comment.CardId), Context.ConnectionId).SendAsync("Send", comment);
        }

        public async Task AddGroup(int cardId)
        {
            string groupName = getGroupName(cardId);
            this.Groups.AddToGroupAsync(Context.ConnectionId,groupName);            
        }

        public async Task SendText(string message)
        {
            await this.Clients.All.SendAsync("Send", message);
        }
    }
}
