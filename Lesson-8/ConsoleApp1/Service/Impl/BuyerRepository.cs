using DocumentFormat.OpenXml.Office2010.Excel;
using Orders.DAL;
using Orders.DAL.Entities;

namespace Client.Service.Impl;

public class BuyerRepository : IBuyerRepository
{
    private readonly OrderDbContext _context;
    public BuyerRepository(OrderDbContext context)
    {
        _context = context;
    }

    public int Create(Buyer entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        return _context.Buyers.Add(entity).Entity.Id;
    }

    public bool Delete(int id)
    {
        var entity = _context.Buyers.FirstOrDefault(buyer => buyer.Id == id);

        if (entity is null)
        {
            return false;
        }

        _context.Buyers.Remove(entity);

        return true;
    }

    public IList<Buyer> GetAll()
    {
        return _context.Buyers.ToList();
    }

    public Buyer GetById(int id)
    {
        return _context.Buyers.FirstOrDefault(buyer => buyer.Id == id);
    }

    public bool Upadte(Buyer entity)
    {
        var buyer = _context.Buyers.FirstOrDefault(buyer => buyer.Id == entity.Id);

        if (buyer is null)
        {
            return false;
        }

        _context.Buyers.Update(entity);

        return true;
    }
}