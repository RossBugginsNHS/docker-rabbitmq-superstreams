using RabbitMQ.Stream.Client;
using System.Net;

public static class SuperDefaults
{
    public async static Task<StreamSystemConfig> DefaultConfig()
    {
        var hn = "rabbitmq";
        var al = await Dns.GetHostEntryAsync(hn);
        var eps = new List<EndPoint>();
        foreach (var addr in al.AddressList)
        {
            Console.WriteLine($"Found {addr} for {hn}");
            eps.Add(new IPEndPoint(addr, 5552));
        }

        var config = new StreamSystemConfig
        {
            UserName = "admin",
            Password = "admin",
            VirtualHost = "/",
            Endpoints = eps
        };

        Console.WriteLine($"Connected to {hn}");
        return config;
    }
}
