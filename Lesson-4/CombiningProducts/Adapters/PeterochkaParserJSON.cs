using CombiningProducts.Models;
using System.Text.Json.Nodes;

namespace CombiningProducts.Adapters;

public class PeterochkaParserJSON : IParseJSONToObject
{
    public Product Parse(JsonNode node)
    {
        return new Product
        {
            Id = node["Index"]!.GetValue<int>(),
            Name = node["ProductName"]!.GetValue<string>(),
            Description = node["Desc"]!.GetValue<string>(),
            Cost = node["Total"]!.GetValue<decimal>(),
        };
    }
}
