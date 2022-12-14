using System.Text.Json.Nodes;

namespace CombiningProducts.Loaders;

public class LoaderFileJSON : ILoader
{
    public JsonNode Load(string path)
    {
        return JsonNode.Parse(File.ReadAllText(path))!;
    }
}
