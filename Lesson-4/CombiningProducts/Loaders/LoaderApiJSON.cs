using System.Text.Json.Nodes;

namespace CombiningProducts.Loaders;

public class LoaderApiJSON : ILoader
{
    public JsonNode Load(string path)
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = client.GetAsync(path).Result;
        response.EnsureSuccessStatusCode();
        string responseBody = response.Content.ReadAsStringAsync().Result;
        JsonNode responseNode = JsonNode.Parse(responseBody)!;
        string jsonProduct = responseNode["data"]![0]!.ToJsonString();

        return JsonNode.Parse(jsonProduct)!;
    }
}
