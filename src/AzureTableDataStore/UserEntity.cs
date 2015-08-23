using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureTableDataStore
{
    public class UserEntity : TableEntity, IUserEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAvailable { get; set; }
        public string ClientId { get; set; }

        public UserEntity()
        { }

        public UserEntity(string firstName, string lastName, bool isAvailable, string clientId)
        {
            PartitionKey = firstName;
            RowKey = lastName;
            FirstName = firstName;
            LastName = lastName;
            IsAvailable = isAvailable;
            ClientId = clientId;
        }

        public IList<IUserEntity> GetUserList()
        {
            throw new NotImplementedException();
        }
    }
}
