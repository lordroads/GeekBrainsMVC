using Orders.DAL.Entities;

namespace Client.Service;

public interface IProductService
{
    Task<Product> CreateAsync(decimal price, string categoryName, string productName);
}