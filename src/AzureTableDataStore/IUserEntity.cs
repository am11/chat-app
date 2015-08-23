namespace AzureTableDataStore
{
    public interface IUserEntity
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        bool IsAvailable { get; set; }
        string ClientId { get; set; }
    }
}
