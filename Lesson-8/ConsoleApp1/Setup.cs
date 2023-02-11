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
    private static WebApplication? _app;
    public static WebApplication App
    {
        //_host ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        get
        {
            if (_app is null)
            {
                _app = CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

                if (!_app.Environment.IsDevelopment())
                {
                    _app.UseDeveloperExceptionPage();
                    //_app.UseExceptionHandler("/Home/Error");
                }
                _app.UseStaticFiles();

                _app.UseRouting();

                _app.UseAuthorization();

                _app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            }

            return _app;
        }
    }
    public static IServiceProvider Services => App.Services;

    public static WebApplicationBuilder CreateHostBuilder(string[] args)
    {
        var webApplicationBuilder = WebApplication.CreateBuilder(args);
        webApplicationBuilder.Host
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(container =>
            {
                container.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
                container.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
                container.RegisterType<BuyerRepository>().As<IBuyerRepository>().InstancePerLifetimeScope();
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

        return webApplicationBuilder;
    }

    private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
    {
        services.AddControllersWithViews();

        #region Configure EF DBContext Srvice

        services.AddDbContext<OrderDbContext>(options =>
        {
            options.UseSqlServer(host.Configuration["Settings:DatabaseOptions:ConnectionString"]);
        });

        #endregion
    }
}