using System.Text.Json.Nodes;

namespace CombiningProducts.Loaders;

public class LoaderStringJSON : ILoader
{
    public JsonNode Load(string path)
    {
        return JsonNode.Parse(path)!;
    }
}
