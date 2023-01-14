using Orders.DAL.Entities;

namespace Client.Service;

public interface IOrderService
{
    Task<Order> CreateAsync(int buyerId, string address, string phone, IEnumerable<(int productId, int quantity)> products);

}