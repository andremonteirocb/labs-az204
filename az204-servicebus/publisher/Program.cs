using Azure.Messaging.ServiceBus;
namespace MessagePublisher
{
    public class Program
    {
        static string serviceBusConnectionString = "servicehub_connection_string";
        private const string queueName = "queue_name";
        private const int numOfMessages = 3;
        static ServiceBusClient client = default!;
        static ServiceBusSender sender = default!;
        
        public static async Task Main(string[] args)
        {
            client = new ServiceBusClient(serviceBusConnectionString);
            sender = client.CreateSender(queueName);
            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();
            for (int i = 1; i <= numOfMessages; i++)
            {
                if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")))
                {
                    throw new Exception($"The message {i} is too large to fit in the batch.");
                }
            }
            try
            {
                await sender.SendMessagesAsync(messageBatch);
                Console.WriteLine($"A batch of {numOfMessages} messages has been published to the queue.");
            }
            finally
            {
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}