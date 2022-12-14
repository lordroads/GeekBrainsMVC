using CombiningProducts.Adapters;
using CombiningProducts.Loaders;
using CombiningProducts.Models;

namespace CombiningProducts.Facades;

public interface IProductCreater
{
    ILoader loader { get; set; }
    IParseJSONToObject parseJSONToObject { get; set; }

    Product CreateProduct(string path);
}
