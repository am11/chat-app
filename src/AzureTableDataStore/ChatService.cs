using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureTableDataStore
{
    public class ChatService : IChatService
    {
        private static readonly TableDataStore _userTable = new TableDataStore("users");
        private static readonly TableDataStore _messageTable = new TableDataStore("messages");

        public IUserEntity SignOnUser(string firstName, string lastName, string clientId)
        {
            return _userTable.CreateOrUpdate(new UserEntity(firstName, lastName, true, clientId)) as IUserEntity;
        }

        public IUserEntity SignOffUser(string firstName, string lastName, string clientId)
        {
            return _userTable.CreateOrUpdate(new UserEntity(firstName, lastName, false, clientId)) as IUserEntity;
        }

        public IMessageEntity PersistMessage(string firstName, string lastName, string message)
        {
            var entity = new MessageEntity(firstName, lastName, message, DateTime.UtcNow);
            return _messageTable.CreateOrUpdate(entity) as IMessageEntity;
        }

        public IEnumerable<IUserEntity> ListUsers()
        {
            return _userTable.RetrieveAll<UserEntity>(new TableQuery<UserEntity>())
                   .Where(u => u.IsAvailable);
        }
    }
}
