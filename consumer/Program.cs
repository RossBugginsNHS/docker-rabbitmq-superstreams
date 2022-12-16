using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

try
{
     var config = await SuperDefaults.DefaultConfig();
    await using var consumersGroup1 = new SuperConsumer(){StreamSystemConfig = config};
    await consumersGroup1.Connect();
    var t = RandomFail();
    var hb = new HostBuilder();
    hb.UseConsoleLifetime();
    var app = hb.Build();
    var appTask = app.RunAsync();

    var any = await Task.WhenAny(t, appTask);
    await any;
}

catch (Exception ex)
{
    Console.WriteLine("Error:\t" + ex.Message);
}
finally
{

}

async Task RandomFail()
{
    Random r = new Random();
    await Task.Delay(r.Next(0,60)*1000);
    throw new Exception("Oops this is a forced crash!");
}