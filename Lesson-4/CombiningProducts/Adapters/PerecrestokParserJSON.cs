using CombiningProducts.Models;
using System.Text.Json.Nodes;

namespace CombiningProducts.Adapters;

public class PerecrestokParserJSON : IParseJSONToObject
{
    public Product Parse(JsonNode node)
    {
        return new Product { 
            Id = node["ID"]!.GetValue<int>(),
            Name = node["Name"]!.GetValue<string>(),
            Description = node["Descroption"]!.GetValue<string>(),
            Cost = node["Cost"]!.GetValue<decimal>(),
        };
    }
}
