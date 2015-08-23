using System;

namespace AzureTableDataStore
{
    public interface IMessageEntity
    {
        string FirstName { get; }
        string LastName { get; }
        string Message { get; }
        DateTime TimeStamp { get; }
    }
}
