using Microsoft.EntityFrameworkCore;
using Orders.DAL.Entities;
using Orders.DAL;

namespace Client;

internal class Program_01
{
    static void Main(string[] args)
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<OrderDbContext>()
            .UseSqlServer("data source=DESKTOP-KU9N192\\SQLEXPRESS; initial catalog=OrdersDatabase; User Id=OrderDbUser; Password=12345; MultipleActiveResultSets=True; App=EntityFramework; TrustServerCertificate=True");

        using (var context = new OrderDbContext(dbContextOptionsBuilder.Options))
        {
            context.Database.EnsureCreated();

            if (!context.Buyers.Any())
            {
                context.Buyers.Add(new Buyer
                {
                    LastName = "Архипова",
                    Name = "Амина",
                    Patronymic = "Алексеевна",
                    Birthday = DateTime.Now.AddYears(-30).Date
                });
                context.Buyers.Add(new Buyer
                {
                    LastName = "Худяков",
                    Name = "Тимофей",
                    Patronymic = "Сергеевич",
                    Birthday = DateTime.Now.AddYears(-35).Date
                });
                context.Buyers.Add(new Buyer
                {
                    LastName = "Прокофьев",
                    Name = "Даниил",
                    Patronymic = "Артёмович",
                    Birthday = DateTime.Now.AddYears(-33).Date
                });
                context.Buyers.Add(new Buyer
                {
                    LastName = "Михайлов",
                    Name = "Иван",
                    Patronymic = "Владимирович",
                    Birthday = DateTime.Now.AddYears(-34).Date
                });
                context.Buyers.Add(new Buyer
                {
                    LastName = "Филимонов",
                    Name = "Виктор",
                    Patronymic = "Константинович",
                    Birthday = DateTime.Now.AddYears(-40).Date
                });

                context.SaveChanges();
            }

            foreach (var buyer in context.Buyers)
            {
                Console.WriteLine(buyer);
            }
        }

        Console.ReadLine();
    }
}
