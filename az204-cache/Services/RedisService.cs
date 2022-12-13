using StackExchange.Redis;

public interface IRedisService
{
     Task<RedisResult> Execute(string command);
    Task<string> Get(string chave);
    Task<bool> Set(string chave, string valor);
    Task<bool> Delete(string chave);
    IDatabase GetDatabase();
}

public class RedisService : IRedisService
{
    private readonly ConnectionMultiplexer _connection;
    public RedisService(ConnectionMultiplexer connection)
    {
        _connection = connection;
    }

    public async Task<string> Get(string chave)
    {
        IDatabase db = _connection.GetDatabase();
        RedisValue redisValue = await db.StringGetAsync(chave);
        return redisValue.ToString();
    }

    public async Task<bool> Set(string chave, string valor)
    {
        IDatabase db = _connection.GetDatabase();
        return await db.StringSetAsync(chave, valor);
    }

    public async Task<bool> Delete(string chave)
    {
        IDatabase db = _connection.GetDatabase();
        return await db.KeyDeleteAsync(chave, CommandFlags.None);
    }

    public async Task<RedisResult> Execute(string command)
    {
        IDatabase db = _connection.GetDatabase();
        return await db.ExecuteAsync(command);
    }

    public IDatabase GetDatabase()
    {
        return _connection.GetDatabase();
    }
}