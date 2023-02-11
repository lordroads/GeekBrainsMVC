using Client.Service;
using Microsoft.Extensions.Logging;
using Orders.DAL;
using Orders.DAL.Entities;

namespace Client.Service.Impl;

public class ProductService : IProductService
{
    private readonly OrderDbContext _context;
    private readonly ILogger<OrderService> _logger;

    public ProductService(OrderDbContext context, ILogger<OrderService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Product> CreateAsync(decimal price, string categoryName, string productName)
    {
        Product product = new Product
        {
            Price = price,
            Name = productName,
            Category = categoryName
        };

        await _context.Products.AddAsync(product);

        await _context.SaveChangesAsync();

        return product;
    }
}