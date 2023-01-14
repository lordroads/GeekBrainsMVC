using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orders.DAL;
using Client.Service.Impl;
using Client.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Client;

public class Setup
{
    private static IHost? _host;
    public static IHost Hosting => _host ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
    public static IServiceProvider Services => Hosting.Services;

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host
            .CreateDefaultBuilder(args)

            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(container =>
            {
                container.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
                container.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
                container.RegisterType<ProductReportWord>().As<IProductReport>().WithParameter("templateFile", "Templates/TemplateProductCatalog.docx").InstancePerLifetimeScope();
                container.RegisterType<OrderReportWord>().As<IOrderReport>().WithParameter("templateFile", "Templates/TemplateOrderReport.docx").InstancePerLifetimeScope();
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
}