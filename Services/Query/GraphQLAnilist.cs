using System.Net.Http.Headers;
using CsAnilist.Models;
using CsAnilist.Models.Character;
using CsAnilist.Models.Media;
using CsAnilist.Models.MediaList;
using CsAnilist.Models.Staff;
using CsAnilist.Models.Studio;
using CsAnilist.Models.User;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class GraphQLAnilist
{
    private readonly string graphQLUrl = "https://graphql.anilist.co";
    private readonly HttpClient _client;

    public GraphQLAnilist()
    {
        _client = new HttpClient();
    }

    public async Task<T> PostAsync<T>(string query, object variables, string dataField)
    {
        var requestBody = new
        {
            query = query,
            variables = variables
        };

        var jsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None
        };

        var jsonContent = JsonConvert.SerializeObject(requestBody, jsonSettings);
        var httpContent = new StringContent(jsonContent);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await _client.PostAsync(graphQLUrl, httpContent);
        var responseBody = await response.Content.ReadAsStringAsync();
        
        response.EnsureSuccessStatusCode();
        
        var jsonResponse = JObject.Parse(responseBody);

        var dataJson = jsonResponse["data"]?[dataField];
        if (dataJson == null)
        {
            throw new Exception("Cant find response data");
        }

        return dataJson.ToObject<T>();
    }

    public Task<AniMedia> GetMediaAsync(string query, object variables)
    {
        return PostAsync<AniMedia>(query, variables, "Media");
    }

    public Task<AniCharacter> GetCharacterAsync(string query, object variables)
    {
        return PostAsync<AniCharacter>(query, variables, "Character");
    }

    public Task<AniStaff> GetStaffAsync(string query, object variables)
    {
        return PostAsync<AniStaff>(query, variables, "Staff");
    }

    public Task<AniStudio> GetStudioAsync(string query, object variables)
    {
        return PostAsync<AniStudio>(query, variables, "Studio");
    }

    public Task<AniUser> GetUserAsync(string query, object variables)
    {
        return PostAsync<AniUser>(query, variables, "User");
    }

    public Task<T> GetPagedDataAsync<T>(string query, object variables)
    {
        return PostAsync<T>(query, variables, "Page");
    }

    public Task<MediaListCollection> GetMediaListCollectionAsync(string query, object variables)
    {
        return PostAsync<MediaListCollection>(query, variables, "MediaListCollection");
    }
}