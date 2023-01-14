using Client;
using Client.Models.Reports;
using Client.Service;
using Client.Extentions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orders.DAL;
using Orders.DAL.Entities;

internal class Program
{
    public static IHost Hosting { get; set; } = Setup.Hosting;
    public static IServiceProvider Services { get; set; } = Setup.Services;

    public static Random random = new Random();

    private static async Task Main(string[] args)
    {
        await Hosting.StartAsync();
        //await PrintBuyersAsync();
        //await CreateProduct();
        //await CreateOrder();
        //await GenerateProductCatalog();
        await GenerateOrderByBuyerId(10);


        Console.ReadKey(true);
        await Hosting.StopAsync();
    }

    private static async Task GenerateOrderByBuyerId(int buyerId)
    {
        await using var serviceScope = Services.CreateAsyncScope();

        var services = serviceScope.ServiceProvider;

        var context = services.GetRequiredService<OrderDbContext>();

        var order = context.Orders.FirstOrDefault(order => order.Buyer.Id == buyerId);
        if (order is null)
        {
            throw new NotImplementedException();
        }
        order.Buyer = context.Buyers.FirstOrDefault(buyer => buyer.Id == buyerId);
        order.Items = context.OrderItems
            .Where(item => item.Order.Id == order.Id)
            .ToList();

        foreach (var item in order.Items)
        {
            item.Product = context.Products.FirstOrDefault(p => p.Id == item.Id);
        }

        var orderReport = new OrderReport
        {
            OrderId = new Random().Next(int.MaxValue/2, int.MaxValue),
            Order = order
        };

        var orderGenerator = services.GetRequiredService<IOrderReport>();

        CreateOrderReport(orderGenerator, orderReport, $"Order_{orderReport.Order.Buyer.LastName}.docx");
    }

    private static void CreateOrderReport(IOrderReport orderGenerator, OrderReport orderReport, string reportFileName)
    {
        orderGenerator.ReportId = orderReport.OrderId;
        orderGenerator.CreationDate = orderReport.Order.OrderDate;
        orderGenerator.BuyerName = orderReport.Order.Buyer.ToString();
        orderGenerator.BuyerAddress = orderReport.Order.Address;
        orderGenerator.BuyerPhone = orderReport.Order.Phone;
        orderGenerator.Products = orderReport.Order.Items.Select(item => (item.Product.Name, item.Quantity, item.Product.Price)).ToList();

        var reportFileInfo = orderGenerator.Create(reportFileName);

        reportFileInfo.Execut();
    }

    private static async Task GenerateProductCatalog()
    {
        await using var serviceScope = Services.CreateAsyncScope();

        var services = serviceScope.ServiceProvider;

        var context = services.GetRequiredService<OrderDbContext>();

        var catalog = new ProductCatalog
        {
            Name = "Каталог товаров.",
            Description = "Актуальный список товаро на дату.",
            CreationDate = DateTime.Now,
            Products = context.Products
        };

        var productReportWord = services.GetRequiredService<IProductReport>();

        CreateReport(productReportWord, catalog, "ReportProductCatalog.docx");
    }
    /// <summary>
    /// Вспомогательный метод для создания файла отчета.
    /// </summary>
    /// <param name="reportGenerator">Объект - генератор отчета.</param>
    /// <param name="catalog">Объект с данными.</param>
    /// <param name="reportFileName">Путь файлы отчета.</param>
    private static void CreateReport(IProductReport reportGenerator, ProductCatalog catalog, string reportFileName)
    {
        reportGenerator.CatalogName = catalog.Name;
        reportGenerator.CatalogDescription = catalog.Description;
        reportGenerator.CreationDate = catalog.CreationDate;
        reportGenerator.Products = catalog.Products.Select(product => (product.Id, product.Name, product.Category, product.Price));

        var reportFiliInfo = reportGenerator.Create(reportFileName);

        reportFiliInfo.Execut();
    }

    private static async Task PrintBuyersAsync()
    {
        await using var serviceScope = Services.CreateAsyncScope();

        var services = serviceScope.ServiceProvider;

        var context = services.GetRequiredService<OrderDbContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        foreach (var buyer in context.Buyers)
        {
            logger.LogInformation(buyer.ToString());
        }
    }
    private static async Task CreateProduct()
    {
        await using var serviceScope = Services.CreateAsyncScope();

        var services = serviceScope.ServiceProvider;

        var productService = services.GetRequiredService<IProductService>();

        await productService.CreateAsync(15000M, "Бетовая техника", "Микроволновая печь Samsung");
    }
    private static async Task CreateOrder()
    {
        await using var serviceScope = Services.CreateAsyncScope();
        var services = serviceScope.ServiceProvider;

        var orderService = services.GetRequiredService<IOrderService>();

        await orderService.CreateAsync(random.Next(1, 11), "123456, Russia, Address", "+79001112233", new (int, int)[]
        {
            new ValueTuple<int, int>(random.Next(1, 2), 1)
        });
    }
}