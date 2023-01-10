using CombiningProducts.Adapters;
using CombiningProducts.Loaders;
using CombiningProducts.Models;

namespace CombiningProducts.Facades;

public class ProductCreater : IProductCreater
{
    public ILoader loader { get; set; }
    public IParseJSONToObject parseJSONToObject { get; set; }

    public ProductCreater(ILoader loader, IParseJSONToObject parseJSONToObject)
    {
        this.loader = loader;
        this.parseJSONToObject = parseJSONToObject;
    }

    public Product CreateProduct(string path)
    {
        return parseJSONToObject.Parse(loader.Load(path));
    }
}
