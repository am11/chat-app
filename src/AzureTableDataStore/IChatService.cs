using System.Collections.Generic;

namespace AzureTableDataStore
{
    public interface IChatService
    {
        IUserEntity SignOnUser(string firstName, string lastName, string clientId);
        IUserEntity SignOffUser(string firstName, string lastName, string clientId);
        IMessageEntity PersistMessage(string firstName, string lastName, string message);
        IEnumerable<IUserEntity> ListUsers();
    }
}
