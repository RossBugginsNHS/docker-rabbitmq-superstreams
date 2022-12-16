using RabbitMQ.Stream.Client;
using System.Net;

public static class SuperDefaults
{
    public async static Task<StreamSystemConfig> DefaultConfig()
    {
        var al = await Dns.GetHostEntryAsync("rabbitmq");
        var a = al.AddressList.First();
        Console.WriteLine($"Will connect to {a}");
        var config = new StreamSystemConfig
        {
            UserName = "admin",
            Password = "admin",
            VirtualHost = "/",
            Endpoints = new List<EndPoint>() { new IPEndPoint(a, 5552) }
        };
        return config;
    }
}
