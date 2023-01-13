using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orders.DAL;
using Orders.DAL.Entities;

namespace Client.Service.Impl;

public class OrderService : IOrderService
{
    private readonly OrderDbContext _context;
    private readonly ILogger<OrderService> _logger;

    public OrderService(OrderDbContext context, ILogger<OrderService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Order> CreateAsync(int buyerId, string address, string phone, IEnumerable<(int productId, int quantity)> products)
    {
        var buyer = await _context.Buyers.FirstOrDefaultAsync(buyer => buyer.Id == buyerId);
        if (buyer == null)
        {
            throw new Exception("Buyer not found.");
        }

        Dictionary<Product, int> productCollection = new Dictionary<Product, int>();

        foreach (var p in products)
        {
            var productEntity = await _context.Products.FirstOrDefaultAsync(product=> product.Id == p.productId);
            if (productEntity == null)
            {
                throw new Exception("Product not found.");
            }
            if (productCollection.ContainsKey(productEntity))
            {
                productCollection[productEntity] += p.quantity;
            }
            else
            {
                productCollection.Add(productEntity, p.quantity);
            }
        }

        var order = new Order
        {
            Buyer = buyer,
            Address = address,
            Phone = phone,
            OrderDate = DateTime.Now,
            Items = productCollection.Select(p => new OrderItem
            {
                Product = p.Key,
                Quantity = p.Value,
            }).ToArray()
        };

        await _context.Orders.AddAsync(order);

        await _context.SaveChangesAsync();

        return order;
    }
}