using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orders.DAL;

namespace Client;

internal class Program_03
{
    private static IHost? _host;

    public static IHost Hosting => _host ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
    public static IServiceProvider Service => Hosting.Services;

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host
            .CreateDefaultBuilder(args)

            .ConfigureHostConfiguration(options =>
                options.AddJsonFile("appsettings.json"))

            .ConfigureAppConfiguration(options =>
                options
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .AddCommandLine(args))

            .ConfigureLogging(options =>
                options
                    .ClearProviders()
                    .AddConsole()
                    .AddDebug())

            .ConfigureServices(ConfigureServices);
    }

    private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
    {
        #region Configure EF DBContext Srvice

        services.AddDbContext<OrderDbContext>(options =>
        {
            options.UseSqlServer(host.Configuration["Settings:DatabaseOptions:ConnectionString"]);
        });

        #endregion
    }

    static async Task Main(string[] args)
    {
        var host = Hosting;
        await host.StartAsync();
        await PrintBuyersAsync();
        Console.ReadKey(true);
        await host.StopAsync();
    }

    private static async Task PrintBuyersAsync()
    {
        await using var serviceScope = Service.CreateAsyncScope();

        var services = serviceScope.ServiceProvider;

        var context = services.GetRequiredService<OrderDbContext>();
        var logger = services.GetRequiredService<ILogger<Program_03>>();

        foreach (var buyer in context.Buyers)
        {
            logger.LogInformation(buyer.ToString());
        }
    }
}
