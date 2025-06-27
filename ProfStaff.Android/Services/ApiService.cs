using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;
using ProfStaff.Models;
using System.Text;

public class ApiService
{
    private const string BaseUrl = "http://p.inventsoft.ru/Locations/GetLocations";
    private const string AuthToken = "ВАШ API";
    private const int ClientId = "ВАШ ID";

    public async Task<List<Location>> GetLocationsAsync()
    {
        using (var client = new HttpClient())
        {
            // Èñïîëüçóåì ïîäõîä èç Form1.cs ñ JSON-òåëîì çàïðîñà
            client.DefaultRequestHeaders.Add("Authorization", AuthToken);
            var requestData = new { clientid = ClientId };
            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(BaseUrl, content);

            if (!response.IsSuccessStatusCode)
                return new List<Location>();

            var responseJson = await response.Content.ReadAsStringAsync();
            var locations = JsonSerializer.Deserialize<List<Location>>(responseJson) ?? new List<Location>();

            // Èñïîëüçóåì ìåòîä ïîñòðîåíèÿ äåðåâà èç Form1.cs
            return BuildLocationTree(locations);
        }
    }

    private List<Location> BuildLocationTree(List<Location> flatLocations)
    {
        var tree = new List<Location>();
        var nodeDictionary = flatLocations.ToDictionary(x => x.Id);

        foreach (var location in flatLocations)
        {
            if (location.Parent_ID.HasValue &&
                nodeDictionary.TryGetValue(location.Parent_ID.Value, out var parent))
            {
                parent.Children.Add(location);
            }
            else if (!location.Parent_ID.HasValue)
            {
                tree.Add(location);
            }
        }

        // Óñòàíàâëèâàåì óðîâíè âëîæåííîñòè
        SetLevels(tree, 0);
        return tree;
    }

    private void SetLevels(List<Location> nodes, int level)
    {
        foreach (var node in nodes)
        {
            node.Level = level;
            SetLevels(node.Children, level + 1);
        }
    }
}
