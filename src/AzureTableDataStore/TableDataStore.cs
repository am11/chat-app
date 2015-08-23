using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Configuration;
using System;

namespace AzureTableDataStore
{
    public class TableDataStore
    {
        private readonly CloudTable _table;

        private static readonly CloudStorageAccount _storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

        private static readonly CloudTableClient _tableClient = _storageAccount.CreateCloudTableClient();

        public TableDataStore(string tableName)
        {
            _table = _tableClient.GetTableReference(tableName);
            _table.CreateIfNotExists();
        }

        public TableEntity CreateOrUpdate(TableEntity entity)
        {
            TableOperation operation = TableOperation.InsertOrReplace(entity);

            return _table.Execute(operation).Result as TableEntity;
        }

        public TableEntity Retrieve(TableEntity entity)
        {
            TableOperation operation = TableOperation.Retrieve<TableEntity>(entity.PartitionKey, entity.RowKey);

            return _table.Execute(operation).Result as TableEntity;
        }

        public IEnumerable<T> RetrieveAll<T>(TableQuery<T> query) where T : ITableEntity, new()
        {
            return _table.ExecuteQuery(query);
        }
    }
}
