using CsAnilist.Models.Enums;
using CsAnilist.Services;
using NUnit.Framework.Legacy;

namespace cs_anilist.test
{
    [TestFixture]
    public class UserTest
    {
        private CsAniListService _anilistService;

        [SetUp]
        public void Setup()
        {
            _anilistService = new CsAniListService();
        }

        [Test]
        public async Task SearchUserAsyncByName()
        {
            string userName = "nupniichan";

            Console.WriteLine($"Testing search for user name: '{userName}'");

            var result = await _anilistService.SearchUserAsync(userName);

            // Debug info
            Console.WriteLine($"Found user: {result.name} (ID: {result.id})");
            
            if (!string.IsNullOrEmpty(result.about))
            {
                Console.WriteLine($"About preview: {result.about.Substring(0, Math.Min(100, result.about.Length))}...");
            }
            
            Console.WriteLine($"Anime Count: {result.statistics?.anime?.count ?? 0}");
            Console.WriteLine($"Manga Count: {result.statistics?.manga?.count ?? 0}");

            ClassicAssert.NotNull(result);
            ClassicAssert.Greater(result.id, 0);
            ClassicAssert.AreEqual(userName, result.name);
        }

        [Test]
        public async Task SearchUserAsyncWithDescriptionAsHtml()
        {
            string userName = "AniList";
            bool descriptionAsHtml = true;

            Console.WriteLine($"Testing search for user with HTML description");

            var result = await _anilistService.SearchUserAsync(userName, descriptionAsHtml);

            // Debug info
            Console.WriteLine($"Found user: {result.name}");
            
            if (!string.IsNullOrEmpty(result.about))
            {
                Console.WriteLine($"Description preview: {result.about.Substring(0, Math.Min(100, result.about.Length))}...");
                Console.WriteLine($"Contains HTML tags: {result.about.Contains("<")}");
            }
            else
            {
                Console.WriteLine("No description available");
            }

            ClassicAssert.NotNull(result);
            
            // If description exists, check if it contains HTML tags when requested
            if (!string.IsNullOrEmpty(result.about) && descriptionAsHtml)
            {
                ClassicAssert.True(result.about.Contains("<"), 
                    "Description should contain HTML tags when descriptionAsHtml is true");
            }
        }

        [Test]
        public async Task GetUserMediaListAsync()
        {
            string userName = "nupniichan";
            var mediaType = MediaType.ANIME;

            Console.WriteLine($"Testing user media list for user: '{userName}', media type: {mediaType}");

            try
            {
                var result = await _anilistService.GetUserMediaListAsync(null, userName, mediaType, (MediaListStatus?)null);

                // Debug info
                Console.WriteLine($"Found media list for user: {userName}");
                Console.WriteLine($"Number of list groups: {result?.lists?.Count ?? 0}");

                if (result?.lists != null && result.lists.Count > 0)
                {
                    foreach (var list in result.lists)
                    {
                        Console.WriteLine($"- List name: {list.name}, entries: {list.entries?.Count ?? 0}");

                        if (list.entries != null && list.entries.Count > 0)
                        {
                            // Show a few example entries
                            for (int i = 0; i < Math.Min(3, list.entries.Count); i++)
                            {
                                var entry = list.entries[i];
                                var title = entry.media?.title?.english ?? entry.media?.title?.romaji ?? "Unknown";
                                Console.WriteLine($"  * {title} - Status: {entry.status}, Score: {entry.score}");
                            }
                        }
                    }
                }

                // Assert
                ClassicAssert.NotNull(result, "Result should not be null");
                ClassicAssert.NotNull(result.lists, "Lists collection should not be null");

                // Only assert if we have actual data
                if (result.lists.Count > 0 && result.lists[0].entries != null && result.lists[0].entries.Count > 0)
                {
                    var firstEntry = result.lists[0].entries[0];
                    ClassicAssert.NotNull(firstEntry.media, "Media should not be null for the first entry");
                    ClassicAssert.NotNull(firstEntry.media.title, "Title should not be null for the first entry's media");
                }
            }
            catch (Exception ex)
            {
                ClassicAssert.Fail($"Test failed with exception: {ex.Message}");
            }
        }

        [Test]
        public async Task GetUserMediaListAsync_WithStatusFilter()
        {
            string userName = "nupniichan";
            var mediaType = MediaType.ANIME;
            MediaListStatus status = MediaListStatus.COMPLETED;

            Console.WriteLine($"Testing user media list with status filter: {status}");

            try
            {
                var result = await _anilistService.GetUserMediaListAsync(null, userName, mediaType, status);

                // Debug info
                Console.WriteLine($"Found media list for user: {userName}, filtered by status: {status}");
                Console.WriteLine($"Number of list groups: {result?.lists?.Count ?? 0}");

                if (result?.lists != null && result.lists.Count > 0)
                {
                    foreach (var list in result.lists)
                    {
                        Console.WriteLine($"- List name: {list.name}, entries: {list.entries?.Count ?? 0}");

                        // Verify entries match the requested status
                        if (list.entries != null && list.entries.Count > 0)
                        {
                            bool allMatchStatus = list.entries.All(entry => 
                                entry.status.HasValue && 
                                entry.status.Value == status);

                            Console.WriteLine($"  All entries match status '{status}': {allMatchStatus}");
                        }
                    }
                }

                ClassicAssert.NotNull(result, "Result should not be null");

                // Only assert on status if we have entries
                if (result.lists != null && result.lists.Count > 0 &&
                    result.lists[0].entries != null && result.lists[0].entries.Count > 0)
                {
                    // Verify all entries in the first list match the requested status
                    ClassicAssert.AreEqual(status, result.lists[0].entries[0].status.Value,
                        $"Entry status should match the requested filter: {status}");
                }
            }
            catch (Exception ex)
            {
                ClassicAssert.Fail($"Test failed with exception: {ex.Message}");
            }
        }

        [Test]
        public async Task UserStatistics_AreRetrieved()
        {
            string userName = "nupniichan";

            Console.WriteLine($"Testing user statistics for: '{userName}'");

            var result = await _anilistService.SearchUserAsync(userName);

            // Debug info
            Console.WriteLine($"User statistics for {result.name}:");
            
            if (result.statistics?.anime != null)
            {
                Console.WriteLine($"Anime statistics:");
                Console.WriteLine($"- Count: {result.statistics.anime.count}");
                Console.WriteLine($"- Mean Score: {result.statistics.anime.meanScore}");
                Console.WriteLine($"- Minutes Watched: {result.statistics.anime.minutesWatched}");
                Console.WriteLine($"- Episodes Watched: {result.statistics.anime.episodesWatched}");
            }
            
            if (result.statistics?.manga != null)
            {
                Console.WriteLine($"Manga statistics:");
                Console.WriteLine($"- Count: {result.statistics.manga.count}");
                Console.WriteLine($"- Mean Score: {result.statistics.manga.meanScore}");
                Console.WriteLine($"- Chapters Read: {result.statistics.manga.chaptersRead}");
                Console.WriteLine($"- Volumes Read: {result.statistics.manga.volumesRead}");
            }

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.statistics);
            
            // User should have anime statistics
            ClassicAssert.NotNull(result.statistics.anime);
            ClassicAssert.GreaterOrEqual(result.statistics.anime.count, 0);
            
            // User should have manga statistics
            ClassicAssert.NotNull(result.statistics.manga);
            ClassicAssert.GreaterOrEqual(result.statistics.manga.count, 0);
        }

        [Test]
        public async Task UserAvatar_ContainsValidUrls()
        {
            // Arrange
            string userName = "AniList";

            Console.WriteLine($"Testing user avatar URLs");

            var result = await _anilistService.SearchUserAsync(userName);

            // Debug info
            Console.WriteLine($"Avatar URLs for {result.name}:");
            
            if (result.avatar != null)
            {
                Console.WriteLine($"- Large: {result.avatar.large ?? "null"}");
                Console.WriteLine($"- Medium: {result.avatar.medium ?? "null"}");
            }
            else
            {
                Console.WriteLine("No avatar available");
            }

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.avatar);
            
            // Avatar should have valid URLs
            if (result.avatar.large != null)
            {
                ClassicAssert.True(result.avatar.large.StartsWith("http"));
            }
            
            if (result.avatar.medium != null)
            {
                ClassicAssert.True(result.avatar.medium.StartsWith("http"));
            }
        }

        // Anilist will return the error code if we spam the request so we have to wait a little
        [OneTimeTearDown]
        public void FreezeTime()
        {
            Thread.Sleep(30000);
        }
    }
} 