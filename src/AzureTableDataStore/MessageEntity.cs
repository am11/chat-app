using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureTableDataStore
{
    public class MessageEntity : TableEntity, IMessageEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Message { get; private set; }
        public DateTime TimeStamp { get; private set; }

        public MessageEntity(string firstName, string lastName, string message, DateTime time)
        {
            PartitionKey = $"{firstName} {lastName}";
            RowKey = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            FirstName = firstName;
            LastName = lastName;
            Message = message;
            TimeStamp = time;
        }
    }
}
