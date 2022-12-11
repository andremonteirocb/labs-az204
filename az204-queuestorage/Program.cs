using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace QueueStorage
{
    public class Program
    {
        static string connectionString = "servicehub_connection_string";
        static string queueName = "queue_name";

        static QueueClient client = default!;
        public static void Main(string[] args)
        {
            // Instantiate a QueueClient which will be used to create and manipulate the queue
            client = new QueueClient(connectionString, queueName);

            CriarFila();
            EnviarMensagem("olá mundo!");
            EspiarMensagem();
            AlterarConteudoMensagem();
            RemoverProximaMensagemFila();
        }

        public static void CriarFila()
        {
            // Create the queue if it doesn't already exist
            client.CreateIfNotExists();
        }
        public static void EnviarMensagem(string message)
        {
            if (client.Exists())
            {
                // Send a message to the queue
                client.SendMessage(message);
            }
        }
        public static void EspiarMensagem()
        {
            if (client.Exists())
            {
                // Peek at the next message
                PeekedMessage[] peekedMessage = client.PeekMessages();
            }
        }
        public static void AlterarConteudoMensagem()
        {
            if (client.Exists())
            {
                // Get the message from the queue
                QueueMessage[] message = client.ReceiveMessages();

                // Update the message contents
                client.UpdateMessage(message[0].MessageId,
                        message[0].PopReceipt,
                        "Updated contents",
                        TimeSpan.FromSeconds(60.0)  // Make it invisible for another 60 seconds
                    );
            }
        }
        public static void RemoverProximaMensagemFila()
        {
            if (client.Exists())
            {
                // Get the next message
                QueueMessage[] retrievedMessage = client.ReceiveMessages();

                if (retrievedMessage.Length > 0)
                {
                    // Process (i.e. print) the message in less than 30 seconds
                    Console.WriteLine($"Dequeued message: '{retrievedMessage[0].Body}'");

                    // Delete the message
                    client.DeleteMessage(retrievedMessage[0].MessageId, retrievedMessage[0].PopReceipt);
                }
            }
        }
    }
}