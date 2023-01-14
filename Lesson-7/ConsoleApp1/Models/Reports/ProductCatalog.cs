using Orders.DAL.Entities;

namespace Client.Models.Reports;

public class ProductCatalog
{
    public string Name { get; set; } = null;
    public string Description { get; set; } = null;
    public DateTime CreationDate { get; set; }
    public IEnumerable<Product> Products { get; set; }
}