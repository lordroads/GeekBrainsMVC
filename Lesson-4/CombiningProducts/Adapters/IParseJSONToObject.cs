using CombiningProducts.Models;
using System.Text.Json.Nodes;

namespace CombiningProducts.Adapters;

public interface IParseJSONToObject
{
    Product Parse(JsonNode node);
}
