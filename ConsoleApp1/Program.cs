using Microsoft.Extensions.DependencyInjection;
using System.Security.Authentication.ExtendedProtection;
using System.Text;

namespace ConsoleApp1;

internal class Program
{
    static void Main(string[] args)
    {
        ServiceCollection serviceCollection = new ();
        serviceCollection.AddTransient<IManager, OrderManager>()
            .AddTimeService();
        ServiceProvider svcProvider = serviceCollection.BuildServiceProvider();
        

        var timeService = svcProvider.GetService<ITimeService>();
        Console.WriteLine($"Time: {timeService?.GetTime()}");

        ShowServices(serviceCollection);
    }


    static void ShowServices(ServiceCollection svcCollection)
    {
        var sb = new StringBuilder();
        foreach(var service in svcCollection)
        {
            sb.Append("Type: ").AppendLine(service.ServiceType.FullName)
            .Append("LifeTyme: ").AppendLine(service.Lifetime.ToString())
            .Append("ImplementationType: ").AppendLine(service.ImplementationType?.FullName)
            .AppendLine();
        }
        Console.WriteLine(sb.ToString());
    }
}


public interface IManager
{

}


public class OrderManager : IManager
{
    
}


interface ITimeService
{
    string GetTime();
}

public static class serviceProciderExtensions
{
    public static void AddTimeService(this IServiceCollection services)
    {
        services.AddTransient<TimeService>();
    }
}

class TimeService : ITimeService
{
    public string GetTime() => DateTime.Now.ToShortTimeString();
}

class LongTimeService : ITimeService
{
    public string GetTime() => DateTime.Now.ToLongTimeString();
}