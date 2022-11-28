using Microsoft.Azure.Cosmos;

public interface ICosmoDbService
{
    Task<Database> CreateDataBase(string databaseId);
    Database GetDataBase(string databaseId);
    Task DeleteDataBase();

    Task<Microsoft.Azure.Cosmos.Container> CreateContainer(string containerId, string partitionKey);
    Microsoft.Azure.Cosmos.Container GetContainer(string containerId);
    Task DeleteContainer(string containerId);

    Task<ItemResponse<T>> CreateItem<T>(string containerId, T input);
    Task<T> GetItem<T>(string containerId, string id);
}
public class CosmoDbService : ICosmoDbService
{
    private string databaseId = "az204Database";
    private CosmosClient _client;
    private Database _database;
    public CosmoDbService(CosmosClient client)
    {
        _client = client;
        _database = client.CreateDatabaseIfNotExistsAsync(databaseId).GetAwaiter().GetResult();
    }

    public async Task<Database> CreateDataBase(string databaseId)
    {
        var dataBaseResponse = await _client.CreateDatabaseIfNotExistsAsync(databaseId);
        return dataBaseResponse.Database;
    }

    public Database GetDataBase(string databaseId)
    {
        var dataBase = _client.GetDatabase(databaseId);
        return dataBase;
    }

    public async Task DeleteDataBase()
    {
        await _database.DeleteAsync();
    }

    public async Task<Microsoft.Azure.Cosmos.Container> CreateContainer(string containerId, string partitionKey)
    {
        var containerResponse = await _database.CreateContainerIfNotExistsAsync(id: containerId, partitionKeyPath: partitionKey);
        return containerResponse.Container;
    }

    public Microsoft.Azure.Cosmos.Container GetContainer(string containerId)
    {
        return _database.GetContainer(containerId);
    }

    public async Task DeleteContainer(string containerId)
    {
        var container = _database.GetContainer(containerId);
        await container.DeleteContainerAsync();
    }

    public async Task<ItemResponse<T>> CreateItem<T>(string containerId, T input)
    {
        var container = _database.GetContainer(containerId);
        var itemResponse = await container.CreateItemAsync(input);
        return itemResponse;
    }

    public async Task<T> GetItem<T>(string containerId, string id)
    {
        var container = _database.GetContainer(containerId);
        var itemResponse = await container.ReadItemAsync<T>(id, new PartitionKey(id));
        return itemResponse.Resource;
    }
}
