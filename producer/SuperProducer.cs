using RabbitMQ.Stream.Client;
using RabbitMQ.Stream.Client.Reliable;
using System.Text;
using RabbitMQ.Stream.Client.AMQP;

public class SuperProducer : IAsyncDisposable
{
    public StreamSystemConfig StreamSystemConfig { get; init; }

    public string Stream { get; init; } = "superstreamtest";

    StreamSystem _system;
    Producer _producer;

    int messageCount = 1_000_000;
    public async Task Connect()
    {
        _system = await StreamSystem.Create(StreamSystemConfig);
        Console.WriteLine("created connection");
        var r = new Random();

        _producer = await Producer.Create(new ProducerConfig(_system, Stream)
        {
            SuperStreamConfig = new SuperStreamConfig()
            {
                Routing = message1 => message1.Properties.MessageId.ToString()
            }
        });

        Console.WriteLine("Producer Created");

        for (var i = 0; i < messageCount; i++)
        {
            var custId = r.Next(0, 100);

            var message = new Message(Encoding.Default.GetBytes($"hello{i}"))
            {
                Properties = new Properties() { MessageId = Guid.NewGuid().ToString() }
            };

            //message.ApplicationProperties = new ApplicationProperties();

           // message.ApplicationProperties.Add("CustomerId", custId.ToString());
          

            await _producer.Send(message);
            Console.WriteLine($"Written Message {i}");
            await Task.Delay(r.Next(100, 1000));
        }

    }

    public async ValueTask DisposeAsync()
    {
        if (_system != null)
            await _system.Close();
        if (_producer != null)
            await _producer.Close();
    }
}
