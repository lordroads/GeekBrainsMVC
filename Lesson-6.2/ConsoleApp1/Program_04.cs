using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Client.Service;
using Client.Service.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orders.DAL;

namespace Client;

internal class Program_04
{
    private static IHost? _host;
    private static Random random = new Random();
    public static IHost Hosting => _host ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
    public static IServiceProvider Service => Hosting.Services;

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host
            .CreateDefaultBuilder(args)

            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(container =>
            {
                container.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
                container.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();

                //TODO: НЕ РАБОТАЕТ! РЕГИСТРИРУЕТ СЕРВИС В КОНТЕЙНЕРЕЕ, ДАЛЕЕ ОН ПРОПАДАЕТ.
                var config = new ConfigurationBuilder()
                    .AddJsonFile("autofac.config.json", false, false);

                var module = new ConfigurationModule(config.Build());
                var builder = new ContainerBuilder();
                builder.RegisterModule(module);

            })

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
        await CreateProduct();
        await CreateOrder();
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

    private static async Task CreateProduct()
    {
        await using var serviceScope = Service.CreateAsyncScope();

        var services = serviceScope.ServiceProvider;

        var productService = services.GetRequiredService<IProductService>();

        await productService.CreateAsync(15000M, "Бетовая техника", "Микроволновая печь Samsung");
    }

    private static async Task CreateOrder()
    {
        await using var serviceScope = Service.CreateAsyncScope();

        var services = serviceScope.ServiceProvider;

        var orderService = services.GetRequiredService<IOrderService>();

        await orderService.CreateAsync(random.Next(1, 11), "123456, Russia, Address", "+79001112233", new (int, int)[]
        {
            new ValueTuple<int, int>(random.Next(1, 2), 1)
        });
    }
}
