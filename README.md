# cs-anilist (Csharp AniList Library)

A simple C# library for getting with the [AniList GraphQL API](https://anilist.co/graphiql), designed for quick access to anime, manga, characters, staff, studios, and user information.

---

## 🚀 Features

- Easy-to-use async methods
- Predefined GraphQL queries
- Supports fetching Anime, Manga, Character, Staff, Studio, and User info
- Returns strongly-typed models

---

## 📦 Installation

### Option 1: Install via NuGet (Recommended)

```bash
Install-Package cs-anilist
```

### Option 2: Manual Installation
Clone this repository or download the ZIP and extract it.

Add the project to your solution:

- In Visual Studio, right-click your solution > Add > Existing Project...

- Select the CsAnilist.csproj file.

Add a project reference:

- Right-click your main project > Add > Reference...

- Check the box for CsAnilist, then click OK.

## 🛠️ Usage
1. Initialize the client
```csharp
using CsAnilist.Services;

var client = new CsAniListService();
```
2. Query Examples

```csharp
// Get anime by ID
var anime = await client.GetMediaAsync(AniQuery.AnimeIDQuery, new { id = 104198, asHtml = true });

// Get character by name
var character = await client.GetCharacterAsync(AniQuery.CharacterSearchQuery, new { search = "Chino Kafuu", asHtml = true });

// Get user by name
var user = await client.GetUserAsync(AniQuery.UserSearchQuery, new { name = "nupniichan", asHtml = true });
```

3. Accessing Response Data
Example response class: AniMedia

```csharp
public class AniMedia
{
    public int id { get; set; }
    public Title title { get; set; }
    public string description { get; set; }
    public CoverImage coverImage { get; set; }
    public int? episodes { get; set; }
    public List<string> genres { get; set; }
    public string siteUrl { get; set; }
}
```
Other models include AniCharacter, AniStaff, AniStudio, AniUser, etc.

## 📋 Predefined GraphQL Queries
```csharp
public static class AniQuery
{
    public const string AnimeIDQuery = @"...";
    public const string CharacterSearchQuery = @"...";
    // more queries...
}
```
🔎 You can customize or create new queries using AniList's GraphQL Explorer.

## 🧪 Example Program
```csharp
using CsAnilist.Services;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new CsAniListService();

        try
        {
            var anime = await client.GetMediaAsync(AniQuery.AnimeNameQuery, new { search = "Is the order a rabbit? Bloom", asHtml = true });

            Console.WriteLine($"ID: {anime.id}");
            Console.WriteLine($"Title: {anime.title.romaji}");
            Console.WriteLine($"Description: {anime.description}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
```

---

## 📖 Documentation
Refer to the official AniList API documentation:
[API Docs](https://anilist.gitbook.io/anilist-apiv2-docs)
[GraphQL Explorer](https://anilist.co/graphiql)

## 📄 License
This project is licensed under the [MIT License](https://github.com/nupniichan/cs-anilist/blob/main/LICENSE).

## 🤝 Contributing
Contributions, issues, and suggestions are welcome! Feel free to open an issue or submit a pull request.
