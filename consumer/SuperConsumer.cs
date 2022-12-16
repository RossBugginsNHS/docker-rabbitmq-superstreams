using RabbitMQ.Stream.Client;
using RabbitMQ.Stream.Client.Reliable;
using System.Text;
using System.Buffers;

public class SuperConsumer : IAsyncDisposable
{
    Random r = new Random();

    public StreamSystemConfig StreamSystemConfig { get; init; }

    public string Stream { get; init; } = "superstreamtest";

    StreamSystem _system;

    Consumer _consumer;

    public async Task Connect()
    {
        _system = await StreamSystem.Create(StreamSystemConfig);
        Console.WriteLine("created connection");
        await ConnectConsumer();
    }


    public async Task<Consumer> ConnectConsumer()
    {
        _consumer = await Consumer.Create(
            new ConsumerConfig(_system, Stream)
            {
            IsSuperStream = true, // Mandatory for enabling the super stream
            IsSingleActiveConsumer = true, // mandatory for enabling the Single Active Consumer
            // this is mandatory for super stream single active consumer
            // must have the same ReferenceName for all the consumers
            Reference = "MyApp",
            OffsetSpec = new OffsetTypeNext(),
            
            
                MessageHandler = async (sourceStream, consumer, ctx, message) =>
                {
                    await MessageHandle(sourceStream, consumer, ctx, message);
                }
            });
        Console.WriteLine($"Consumer created");
        return _consumer;
    }

    public virtual async Task MessageHandle(
        string sourceStream,
        RawConsumer consumer,
        MessageContext ctx,
        Message message)
    {
        var id = message.Properties.MessageId.ToString();
        var custId = "";//message.ApplicationProperties.GetValueOrDefault("CustomerId").ToString();
        Console.WriteLine(
            $"{sourceStream}:\t message for customer {custId}\t coming from Id {id}\t data: {Encoding.Default.GetString(message.Data.Contents.ToArray())} - consumed");
        await Task.Delay(r.Next(100, 1000));
    }


    public async ValueTask DisposeAsync()
    {
        if (_system != null)
            await _system.Close();

        if (_consumer != null)
            await _consumer.Close();

    }
}