using System.Text.Json.Nodes;

namespace CombiningProducts.Loaders;

public interface ILoader
{
    JsonNode Load(string path);
}
