using System.Collections.Generic;
using System.Threading.Tasks;
using AzureTableDataStore;
using Microsoft.AspNet.SignalR;

namespace jutteleminen_sovellus.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        public static Dictionary<string, IUserEntity> Users = new Dictionary<string, IUserEntity>();

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public void SendMessage(string firstName, string lastName, string message)
        {
            IMessageEntity echoedEntity = _chatService.PersistMessage(firstName, lastName, message);

            Clients.All.addNewMessageToPage($"{echoedEntity.FirstName} {echoedEntity.LastName}",
                                            echoedEntity.Message,
                                            echoedEntity.TimeStamp);
        }

        public void AnnounceLogOn(string firstName, string lastName)
        {
            var user = _chatService.SignOnUser(firstName, lastName, Context.ConnectionId);
            Users.Add(Context.ConnectionId, user);
            Clients.All.addNewUserToPage(firstName, lastName, Context.ConnectionId);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var user = Users[Context.ConnectionId];

            Users.Remove(Context.ConnectionId);
            _chatService.SignOffUser(user.FirstName, user.LastName, Context.ConnectionId);
            Clients.All.removeUserFromPage(Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }
    }
}
