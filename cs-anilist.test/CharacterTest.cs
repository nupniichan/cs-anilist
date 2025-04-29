using CsAnilist.Models.Character;
using CsAnilist.Models.Enums;
using CsAnilist.Services;
using NUnit.Framework.Legacy;

namespace cs_anilist.test
{
    // This test class is for testing the character search
    [TestFixture]
    public class CharacterTest
    {
        private CsAniListService _anilistService;

        [SetUp]
        public void Setup()
        {
            _anilistService = new CsAniListService();
        }

        // Search for a character by name
        [Test]
        public async Task SearchCharacterAsyncByName()
        {
            string characterName = "Chino Kafuu";

            Console.WriteLine($"Testing search for character name: '{characterName}'");

            var result = await _anilistService.SearchCharacterAsync(characterName);

            // Debug info
            Console.WriteLine($"Found character: {result.name.full()}");
            Console.WriteLine($"Native name: {result.name.native}");
            Console.WriteLine($"Description: {result.description?.Substring(0, Math.Min(result.description?.Length ?? 0, 100))}...");
            Console.WriteLine($"Favorites: {result.favourites}");

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.name);
            
            // Check if character name matches (could be partial)
            ClassicAssert.True(result.name.full().IndexOf(characterName, StringComparison.OrdinalIgnoreCase) >= 0,
                "Character name should contain the search term");
        }

        [Test]
        public async Task SearchCharacterAsync_WithDescriptionAsHtml()
        {
            string characterName = "Chino Kafuu";
            bool descriptionAsHtml = true;

            Console.WriteLine($"Testing search for character with HTML description");

            var result = await _anilistService.SearchCharacterAsync(characterName, descriptionAsHtml);

            // Debug info
            Console.WriteLine($"Found character: {result.name.first} {result.name.last}");
            
            if (!string.IsNullOrEmpty(result.description))
            {
                Console.WriteLine($"Description preview: {result.description.Substring(0, Math.Min(100, result.description.Length))}...");
                Console.WriteLine($"Contains HTML tags: {result.description.Contains("<")}");
            }
            else
            {
                Console.WriteLine("No description available");
            }

            // ClassicAssert
            ClassicAssert.NotNull(result);
            
            // If description exists, check if it contains HTML tags when requested
            if (!string.IsNullOrEmpty(result.description) && descriptionAsHtml)
            {
                ClassicAssert.True(result.description.Contains("<"), 
                    "Description should contain HTML tags when descriptionAsHtml is true");
            }
        }

        [Test]
        public async Task GetPagedCharactersAsync()
        {
            int page = 1;
            int perPage = 5;

            Console.WriteLine($"Testing paged characters (page: {page}, perPage: {perPage})");

            var result = await _anilistService.GetPagedCharactersAsync(page, perPage);

            // Debug info
            Console.WriteLine($"Retrieved {result.characters?.Count ?? 0} characters");
            Console.WriteLine($"Page info: Current={result.pageInfo.currentPage}, Total={result.pageInfo.total}, HasNext={result.pageInfo.hasNextPage}");
            
            foreach (var character in result.characters)
            {
                Console.WriteLine($"- {character.name.full()}");
                Console.WriteLine($"  Favorites: {character.favourites}");
            }

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.characters);
            ClassicAssert.AreEqual(perPage, result.characters.Count);
            ClassicAssert.NotNull(result.pageInfo);
            ClassicAssert.AreEqual(page, result.pageInfo.currentPage);
        }

        [Test]
        public async Task GetPagedCharactersAsync_WithSearch()
        {
            int page = 1;
            int perPage = 5;
            string search = "Cocoa";

            Console.WriteLine($"Testing paged characters search with term: '{search}'");

            var result = await _anilistService.GetPagedCharactersAsync(page, perPage, search);

            // Debug info
            Console.WriteLine($"Found {result.characters?.Count ?? 0} characters for search '{search}'");
            
            foreach (var character in result.characters)
            {
                Console.WriteLine($"- {character.name.full()}");
            }

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.characters);
            ClassicAssert.True(result.characters.Count > 0);
            
            // Check if at least one result contains the search term
            bool hasMatchingName = false;
            foreach (var character in result.characters)
            {
                if (character.name.full().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    hasMatchingName = true;
                    break;
                }
            }
            
            ClassicAssert.True(hasMatchingName, 
                "At least one character should match the search term");
        }

        [Test]
        public async Task GetPagedCharactersAsync_WithSort()
        {
            int page = 1;
            int perPage = 10;
            string sort = MediaSort.FAVOURITES_DESC.ToString(); 

            Console.WriteLine($"Testing paged characters with sorting: {sort}");

            var result = await _anilistService.GetPagedCharactersAsync(page, perPage, null, sort);

            // Debug info
            Console.WriteLine($"Retrieved {result.characters?.Count ?? 0} characters sorted by favorites");
            
            foreach (var character in result.characters.Take(5))
            {
                Console.WriteLine($"- {character.name.full()}");
                Console.WriteLine($"  Favorites: {character.favourites}");
            }

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.characters);
            
            // Verify popular characters are returned (should have high favorites count)
            ClassicAssert.Greater(result.characters[0].favourites, 10000); 
            
            // Check favorites is actually sorted in descending order
            for (int i = 0; i < result.characters.Count - 1; i++)
            {
                ClassicAssert.GreaterOrEqual(result.characters[i].favourites, result.characters[i + 1].favourites);
            }
        }

        [Test]
        public async Task CharacterImage_ContainsValidUrls()
        {
            string characterName = "Chino Kafuu";

            Console.WriteLine($"Testing character image URLs");

            var result = await _anilistService.SearchCharacterAsync(characterName);

            // Debug info
            Console.WriteLine($"Image URLs for {result.name.first} {result.name.last}:");
            Console.WriteLine($"- Large: {result.image.large ?? "null"}");
            Console.WriteLine($"- Medium: {result.image.medium ?? "null"}");

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.image);
            
            // Image should have valid URLs
            ClassicAssert.False(string.IsNullOrEmpty(result.image.large));
            ClassicAssert.False(string.IsNullOrEmpty(result.image.medium));
            
            // URLs should be actual URLs
            ClassicAssert.True(result.image.large.StartsWith("http"));
            ClassicAssert.True(result.image.medium.StartsWith("http"));
        }

        // Anilist will return the error code if we spam the request so we have to wait a little
        [OneTimeTearDown]
        public void FreezeTime()
        {
            Thread.Sleep(30000);
        }
    }

    // I wanna make full name for character so this function exists ( not in original test )
    public static class CharacterNameExtensions
    {
        public static string full(this CharacterName name)
        {
            if (string.IsNullOrEmpty(name.last))
                return name.first;
            return $"{name.first} {name.last}";
        }
    }
} 