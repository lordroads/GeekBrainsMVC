using CombiningProducts.Adapters;
using CombiningProducts.Enums;
using CombiningProducts.Facades;
using CombiningProducts.Loaders;
using CombiningProducts.Models;

namespace CombiningProducts.Factories;

public static class ProductFactory
{
    public static Product? GetProduct(string path, SourceType sourceType)
    {
        Product product;

        switch (sourceType)
        {
            case SourceType.PEREKRESTOK:
                product = new ProductCreater(new LoaderStringJSON(), new PerecrestokParserJSON()).CreateProduct(path);
                product.SourceType = sourceType;
                return product;
            case SourceType.PETEROCHKA:
                product = new ProductCreater(new LoaderFileJSON(), new PeterochkaParserJSON()).CreateProduct(path);
                product.SourceType = sourceType;
                return product;
            case SourceType.ASHAN:
                product = new ProductCreater(new LoaderApiJSON(), new AshanParserJSON()).CreateProduct(path);
                product.SourceType = sourceType;
                return product;
            default:
                return null;
        }
    }
}
