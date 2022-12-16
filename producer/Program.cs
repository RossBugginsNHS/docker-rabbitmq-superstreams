using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

try
{
    var config = await SuperDefaults.DefaultConfig();
    await using var producer = new SuperProducer(){StreamSystemConfig = config};
    await producer.Connect();

    var hb = new HostBuilder();
    hb.UseConsoleLifetime();
    var app = hb.Build();
    await app.RunAsync();
}

catch (Exception ex)
{
    Console.WriteLine("Error:\t" + ex.Message);
}
finally
{

}
