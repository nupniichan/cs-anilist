# cs-anilist (Csharp AniList Library Unofficial)

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
using CsAnilist.AnilistAPI.Enum;

var client = new CsAniListService();
```

2. Search Examples

```csharp
// Search anime by name
var anime = await client.SearchMedia("Is the order a rabbit?", MediaType.ANIME);

// Search character by name
var character = await client.SearchCharacterAsync("Chino Kafuu");

// Search staff by name
var staff = await client.SearchStaffAsync("Koi");

// Search studio by name
var studio = await client.SearchStudioAsync("ufotable");

// Search user by name
var user = await client.SearchUserAsync("nupniichan");
```

3. Accessing Media Data

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

### New Features

Recent updates include:

- **Character Relationships**: Access character roles and voice actors
- **Studio Relationships**: See which studios are main production studios
- **Staff Roles**: Understand what role each staff member had in a production
- **Extended Media Information**: Tags, popularity metrics, country of origin, etc.
- **Improved Error Handling**: Better handling of missing or incomplete data

## 🧪 Example Program

```csharp
using CsAnilist.Services;
using CsAnilist.AnilistAPI.Enum;
using System;
using System.Threading.Tasks;

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
