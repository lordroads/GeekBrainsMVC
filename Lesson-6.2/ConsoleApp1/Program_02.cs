using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Orders.DAL;

namespace Client;

internal class Program_02
{
    static void Main(string[] args)
    {
        var serviceBuilder = new ServiceCollection();

        #region Configure EF DBContext Srvice

        serviceBuilder.AddDbContext<OrderDbContext>(options =>
        {
            options.UseSqlServer("data source=DESKTOP-KU9N192\\SQLEXPRESS; initial catalog=OrdersDatabase; User Id=OrderDbUser; Password=12345; MultipleActiveResultSets=True; App=EntityFramework; TrustServerCertificate=True");
        });

        #endregion

        var servicesProvider = serviceBuilder.BuildServiceProvider();

        var context = servicesProvider.GetRequiredService<OrderDbContext>();

        foreach (var buyer in context.Buyers)
        {
            Console.WriteLine(buyer);
        }

        Console.ReadKey(true);
    }
}
