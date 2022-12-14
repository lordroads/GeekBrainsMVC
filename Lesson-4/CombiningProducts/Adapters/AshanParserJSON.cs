using CombiningProducts.Models;
using System.Text.Json.Nodes;

namespace CombiningProducts.Adapters;

public class AshanParserJSON : IParseJSONToObject
{
    public Product Parse(JsonNode node)
    {
        return new Product
        {
            Id = node["id"]!.GetValue<int>(),
            Name = node["title"]!.GetValue<string>(),
            Description = node["description"]!.GetValue<string>(),
            Cost = node["price"]!.GetValue<decimal>(),
        };
    }
}
