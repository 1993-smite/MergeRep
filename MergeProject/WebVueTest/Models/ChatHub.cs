using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;


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
            comment.CreatedUser = FactoryUserView.Create(3);
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
