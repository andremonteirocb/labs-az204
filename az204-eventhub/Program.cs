using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs.Producer;
using Azure.Storage.Blobs;
using Newtonsoft.Json;
using System.Text;

public class Program
{
    private const string eventHubsConnectionString = "hub_connection_string";
    private const string eventHubName = "hub_name";

    public static async Task Main(string[] args)
    {
        await ObterPartitions();
        await PublicarEventos();
        await LerEventosDeTodasAsParticoesDoHub();
        await LerEventosDeUmaParticaoDoHub();

        Console.ReadLine();
    }

    public static async Task ObterPartitions()
    {
        await using (var producer = new EventHubProducerClient(eventHubsConnectionString, eventHubName))
        {
            string[] partitionIds = await producer.GetPartitionIdsAsync();
            foreach (var partitionId in partitionIds)
                Console.WriteLine($"partition id: {partitionId}");
        }
    }

    public static async Task PublicarEventos()
    {
        await using (var producer = new EventHubProducerClient(eventHubsConnectionString, eventHubName))
        {
            using EventDataBatch eventBatch = await producer.CreateBatchAsync();
            eventBatch.TryAdd(new EventData(new BinaryData("First")));
            eventBatch.TryAdd(new EventData(new BinaryData("Second")));
            eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { Id = 1, Name = "Tester" }))));

            await producer.SendAsync(eventBatch);
        }
    }

    public static async Task LerEventosDeTodasAsParticoesDoHub()
    {
        string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

        await using (var consumer = new EventHubConsumerClient(consumerGroup, eventHubsConnectionString, eventHubName))
        {
            using var cancellationSource = new CancellationTokenSource();
            cancellationSource.CancelAfter(TimeSpan.FromSeconds(45));

            await foreach (PartitionEvent receivedEvent in consumer.ReadEventsAsync(cancellationSource.Token))
            {
                Console.WriteLine($"received: {receivedEvent.Data.EventBody.ToString()}");
            }
        }
    }

    public static async Task LerEventosDeUmaParticaoDoHub()
    {
        string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

        await using (var consumer = new EventHubConsumerClient(consumerGroup, eventHubsConnectionString, eventHubName))
        {
            EventPosition startingPosition = EventPosition.Earliest;
            string partitionId = (await consumer.GetPartitionIdsAsync()).First();

            using var cancellationSource = new CancellationTokenSource();
            cancellationSource.CancelAfter(TimeSpan.FromSeconds(45));

            await foreach (PartitionEvent receivedEvent in consumer.ReadEventsFromPartitionAsync(partitionId, startingPosition, cancellationSource.Token))
            {
                Console.WriteLine($"received: {receivedEvent.Data.EventBody.ToString()}");
            }
        }
    }

    public static async Task LerEventosEventProcessorClient()
    {
        var cancellationSource = new CancellationTokenSource();
        cancellationSource.CancelAfter(TimeSpan.FromSeconds(45));

        var storageConnectionString = "<< CONNECTION STRING FOR THE STORAGE ACCOUNT >>";
        var blobContainerName = "<< NAME OF THE BLOB CONTAINER >>";
        var consumerGroup = "<< NAME OF THE EVENT HUB CONSUMER GROUP >>";

        Task processEventHandler(ProcessEventArgs eventArgs) => ProcessEventHandler();
        Task processErrorHandler(ProcessErrorEventArgs eventArgs) => ProcessErrorHandler();

        var storageClient = new BlobContainerClient(storageConnectionString, blobContainerName);
        var processor = new EventProcessorClient(storageClient, consumerGroup, eventHubsConnectionString, eventHubName);

        processor.ProcessEventAsync += processEventHandler;
        processor.ProcessErrorAsync += processErrorHandler;

        await processor.StartProcessingAsync();

        try
        {
            await Task.Delay(Timeout.Infinite, cancellationSource.Token);
        }
        catch (TaskCanceledException)
        {
            // This is expected when the delay is canceled.
        }

        try
        {
            await processor.StopProcessingAsync();
        }
        finally
        {
            // To prevent leaks, the handlers should be removed when processing is complete.
            processor.ProcessEventAsync -= processEventHandler;
            processor.ProcessErrorAsync -= processErrorHandler;
        }
    }

    public static Task ProcessEventHandler()
    {
        Console.WriteLine("ProcessEventHandler");
        return Task.CompletedTask;
    }

    public static Task ProcessErrorHandler()
    {
        Console.WriteLine("ProcessErrorHandler");
        return Task.CompletedTask;
    }
}
