using Azure;
using Azure.Core.Serialization;
using Azure.Messaging;
using Azure.Messaging.EventGrid;
using System.Text;
using System.Text.Json;

public class Program
{
    private const string topicEndpoint = "topic_endpoint";
    private const string topicKey = "topic_key";

    public static async Task Main(string[] args)
    {
        await PublishEventGridToEventGridTopic();
    }

    public static async Task PublishEventGridToEventGridTopic()
    {
        Uri endpoint = new Uri(topicEndpoint);
        AzureKeyCredential credential = new AzureKeyCredential(topicKey);
        EventGridPublisherClient client = new EventGridPublisherClient(endpoint, credential);

        EventGridEvent firstEvent = new EventGridEvent(
            subject: $"New Employee: Alba Sutton",
            eventType: "Employees.Registration.New",
            dataVersion: "1.0",
            data: new
            {
                FullName = "Alba Sutton",
                Address = "4567 Pine Avenue, Edison, WA 97202"
            }
        );

        EventGridEvent secondEvent = new EventGridEvent(
            subject: $"New Employee: Alexandre Doyon",
            eventType: "Employees.Registration.New",
            dataVersion: "1.0",
            data: new
            {
                FullName = "Alexandre Doyon",
                Address = "456 College Street, Bow, WA 98107"
            }
        );

        await client.SendEventAsync(firstEvent);
        Console.WriteLine("First event published");

        await client.SendEventAsync(secondEvent);
        Console.WriteLine("Second event published");
    }

    public static async Task PublishCloudEventsToEventGridTopic()
    {
        Uri endpoint = new Uri(topicEndpoint);
        AzureKeyCredential credential = new AzureKeyCredential(topicKey);
        EventGridPublisherClient client = new EventGridPublisherClient(endpoint, credential);

        // Example of a custom ObjectSerializer used to serialize the event payload to JSON
        var myCustomDataSerializer = new JsonObjectSerializer(
            new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

        // Add CloudEvents to a list to publish to the topic
        List<CloudEvent> eventsList = new List<CloudEvent>
        {
            // CloudEvent with custom model serialized to JSON
            new CloudEvent(
                "/cloudevents/example/source",
                "Example.EventType",
                new { A = 5, B = true }),

            // CloudEvent with custom model serialized to JSON using a custom serializer
            new CloudEvent(
                "/cloudevents/example/source",
                "Example.EventType",
                myCustomDataSerializer.Serialize(new { A = 5, B = true }),
                "application/json"),

            // CloudEvents also supports sending binary-valued data
            new CloudEvent(
                "/cloudevents/example/binarydata",
                "Example.EventType",
                new BinaryData(Encoding.UTF8.GetBytes("This is treated as binary data")),
                "application/octet-stream")
        };

        // Send the events
        await client.SendEventsAsync(eventsList);
    }

    public static async Task PublishEventGridToEventGridDomain()
    {
        Uri endpoint = new Uri(topicEndpoint);
        AzureKeyCredential credential = new AzureKeyCredential(topicKey);
        EventGridPublisherClient client = new EventGridPublisherClient(endpoint, credential);

        List<EventGridEvent> eventsList = new List<EventGridEvent>
        {
            new EventGridEvent(
                "ExampleEventSubject",
                "Example.EventType",
                "1.0",
                "This is the event data")
            {
                Topic = "MyTopic"
            }
        };

        await client.SendEventsAsync(eventsList);
    }
}
