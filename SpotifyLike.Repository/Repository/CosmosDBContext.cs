using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Repository.Repository
{
    public class CosmosDBContext
    {

        public CosmosClient Client { get; set; }
        public string AccountEndpoint { get; set; }
        public string TokenCredential { get; set; }
        private Database Database { get; set; }
        private Container Container { get; set; }
        private string ContainerName { get; set; }
        private string DatabaseName = "spotifylikeadmin";

        public CosmosDBContext(IConfiguration configuration)
        {
            AccountEndpoint = configuration["CosmosConnection:AccountEndpoint"]?.ToString();
            TokenCredential = configuration["CosmosConnection:TokenCredential"]?.ToString();
            Client = new CosmosClient(AccountEndpoint, TokenCredential);

            this.Database = this.Client.GetDatabase(DatabaseName);
        }
        public void SetContainer(string containerName)
        {
            this.ContainerName = containerName;
            this.Container = this.Database.GetContainer(this.ContainerName);
        }

        public async Task SaveOrUpdate<T>(T entity, string partitionKey) where T : class
        {
            this.Container.UpsertItemAsync<T>(item: entity, partitionKey: new PartitionKey(partitionKey));
        }

        public async Task<List<T>> ReadAllItems<T>() where T : class
        {
            var query = new QueryDefinition($"SELECT * FROM {this.ContainerName}");

            using FeedIterator<T> resultSet = this.Container.GetItemQueryIterator<T>(query);
            List<T> result = new List<T>();
            while (resultSet.HasMoreResults)
            {
                FeedResponse<T> response = await resultSet.ReadNextAsync();
                result.AddRange(response);
            }

            return result;
        }

        public async Task<T> ReadItem<T>(string id) where T : class
        {
            var query = new QueryDefinition($"SELECT * FROM {this.ContainerName} c WHERE c.id = '{id}'");

            using FeedIterator<T> resultSet = this.Container.GetItemQueryIterator<T>(query);
            List<T> result = new List<T>();
            while (resultSet.HasMoreResults)
            {
                FeedResponse<T> response = await resultSet.ReadNextAsync();
                result.AddRange(response);
            }

            return result.FirstOrDefault();
        }

        public async void Delete<T>(string id, string partitionKey) where T : class
        {
            await this.Container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
        }

    }
}
