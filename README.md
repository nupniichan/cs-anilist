# cs-anilist (Csharp AniList Library Unofficial)

A simple C# library for accessing the [AniList GraphQL API](https://anilist.co/graphiql), designed for quick and easy retrieval of anime, manga, characters, staff, studios, and user information.

---

## 🚀 Features

- Easy-to-use async methods
- Predefined GraphQL queries
- Supports fetching Anime, Manga, Character, Staff, Studio, and User info
- Pagination support for search results
- Media trends and user media lists
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
using CsAnilist.Models.Enums;

var client = new CsAniListService();
```

2. Search Examples

```csharp
// Search anime by name
var anime = await client.SearchMedia("Is the order a rabbit?", MediaType.ANIME);

// Search manga by name
var manga = await client.SearchMedia("Is the order a rabbit?", MediaType.MANGA);

// Search by ID
var animeById = await client.SearchMedia(4181, MediaType.ANIME);
// Note: Manga is the same but you need to change MediaType to MediaType.Manga

// Search character by name
var character = await client.SearchCharacterAsync("Chino Kafuu");

// Search character by ID
var characterById = await client.SearchCharacterAsync(88032);

// Search staff by name
var staff = await client.SearchStaffAsync("Key");

// Search staff by ID
var staffById = await client.SearchStaffAsync(117309);

// Search studio by name
var studio = await client.SearchStudioAsync("Kyoto Animation");

// Search studio by ID
var studioById = await client.SearchStudioAsync(2);

// Search user by name
var user = await client.SearchUserAsync("nupniichan");

// Search user by ID
var userById = await client.SearchUserAsync(5660278);
```

3. Pagination Support

```csharp
// Get paged anime results
var animeResults = await client.GetPagedMediaAsync(1, 10, MediaType.ANIME, "Kyoto Animation");

// Get paged character results
var characterResults = await client.GetPagedCharactersAsync(1, 5, "Miku");

// Get paged staff results
var staffResults = await client.GetPagedStaffAsync(1, 5, "Koi");

// Get paged studio results
var studioResults = await client.GetPagedStudiosAsync(1, 5, "Key", MediaSort.SEARCH_MATCH.ToString());

// Simplified paged media query (without search and using default sort)
var simplePagedResults = await client.GetPagedMediaAsync(1, 10, MediaType.ANIME, MediaSort.SEARCH_MATCH.ToString());
```

4. Media Trends and User Lists

```csharp
// Get media trend information for a specific anime/manga
var mediaTrend = await client.GetMediaTrendAsync(109731);

// Get all media trends for anime (with pagination)
var allAnimeTrends = await client.GetAllMediaTrendsAsync(MediaType.ANIME, 1, 10);

// Get all media trends for manga (with pagination)
var allMangaTrends = await client.GetAllMediaTrendsAsync(MediaType.MANGA, 1, 10);

// Get user's anime list
var userAnimeList = await client.GetUserMediaListAsync(userName: "nupniichan", mediaType: MediaType.ANIME);

// Get user's manga list with status filter (using enum)
var userMangaList = await client.GetUserMediaListAsync(userName: "nupniichan", mediaType: MediaType.MANGA, status: MediaListStatus.COMPLETED);

// Get user's manga list with status filter (using string)
var userMangaListString = await client.GetUserMediaListAsync(userName: "nupniichan", mediaType: MediaType.MANGA, status: "COMPLETED");

// Get user's list by user ID
var userListById = await client.GetUserMediaListAsync(userId: 5660278, mediaType: MediaType.ANIME);
```

5. Accessing Media Data

```csharp
// Basic info
Console.WriteLine($"Title: {anime.title.romaji} ({anime.title.english})");
Console.WriteLine($"Type: {anime.type}, Format: {anime.format}, Status: {anime.status}");
Console.WriteLine($"Episodes: {anime.episodes}, Duration: {anime.duration} minutes");
Console.WriteLine($"Score: {anime.averageScore}/100");

// Get list of anime's character
if (anime.characters?.edges != null) 
{
    foreach (var character in anime.characters.edges)
    {
        Console.WriteLine($"Character: {character.node.name.first} {character.node.name.last}");
        Console.WriteLine($"Role: {character.role}");
        
        // Voice actors
        if (character.voiceActors != null)
        {
            foreach (var actor in character.voiceActors)
            {
                Console.WriteLine($"  Voice Actor: {actor.name.first} {actor.name.last} ({actor.language})");
            }
        }
    }
}

// Get Studio made it
if (anime.studios?.edges != null)
{
    foreach (var studio in anime.studios.edges)
    {
        Console.WriteLine($"Studio: {studio.node.name} (Main: {studio.isMain})");
    }
}
```

## 📋 Available Models

The library includes the following main models:

- **AniMedia**: Anime and manga information (episodes, chapters, genres, studios, characters, etc.)
- **AniCharacter**: Character information (name, description, images)
- **AniStaff**: Staff member information (name, roles, works, characters)
- **AniStudio**: Studio information (name, works)
- **AniUser**: User profile information (favorites, statistics)
- **Page**: Pagination information for search results
- **MediaListCollection**: User's media lists with status information

### Features

- **Character Relationships**: Access character roles and voice actors
- **Studio Relationships**: See which studios are main production studios
- **Staff Roles**: Understand what role each staff member had in a production
- **Media Trends**: Track popularity and trending statistics for media
- **User Media Lists**: Access a user's anime and manga lists with status filtering
- **Pagination**: Efficiently search through large result sets
- **Extended Media Information**: Tags, popularity metrics, country of origin, etc.
- **Improved Error Handling**: Better handling of missing or incomplete data

## 🧪 Example Program

```csharp
using CsAnilist.Services;
using CsAnilist.Models.Enums;
using System;
using System.Threading.Tasks;
using System.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new CsAniListService();

        try
        {
            // Search for anime
            var anime = await client.SearchMedia("Is the order a rabbit? Bloom", MediaType.ANIME);
            
            Console.WriteLine($"Title: {anime.title.romaji} ({anime.title.english})");
            Console.WriteLine($"Episodes: {anime.episodes}");
            Console.WriteLine($"Score: {anime.averageScore}/100");
            
            // Display main characters
            if (anime.characters?.edges != null)
            {
                Console.WriteLine("Main Characters:");
                foreach (var character in anime.characters.edges.Where(c => c.role == "MAIN").Take(3))
                {
                    Console.WriteLine($" - {character.node.name.first} {character.node.name.last}");
                }
            }
            
            // Get paginated results for a broader search
            var searchResults = await client.GetPagedMediaAsync(1, 5, MediaType.ANIME, "rabbit");
            Console.WriteLine($"\nFound {searchResults.pageInfo.total} anime containing 'rabbit':");
            
            foreach (var media in searchResults.media)
            {
                Console.WriteLine($" - {media.title.romaji}");
            }
            
            // Get all anime trends from page 1
            var trends = await client.GetAllMediaTrendsAsync(MediaType.ANIME, 1, 3);
            Console.WriteLine($"\nRecent Trending Anime:");
            
            foreach (var trend in trends.mediaTrends)
            {
                Console.WriteLine($" - {trend.media.title.romaji} (Trending score: {trend.trending})");
            }
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
- [API Docs](https://anilist.gitbook.io/anilist-apiv2-docs)
- [GraphQL Explorer](https://anilist.co/graphiql)

## 📄 License
This project is licensed under the [MIT License](https://github.com/nupniichan/cs-anilist/blob/main/LICENSE).

## 🤝 Contributing
Contributions, issues, and suggestions are welcome! Feel free to open an issue or submit a pull request.
