using CombiningProducts.Enums;

namespace CombiningProducts.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public SourceType SourceType { get; set; }

    public override string ToString()
    {
        return $"Source: {SourceType}\nItem ID: {Id}\nName Product: {Name}\nDescroption: {Description}\nCost: {Cost}\n";
    }
}
