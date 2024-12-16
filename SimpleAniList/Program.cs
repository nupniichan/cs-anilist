using SimpleAnilist.AnilistAPI.Enum;
using SimpleAnilist.Services;

class Program
{
    static async Task Main(string[] args)
    {  
        SimpleAniListService simpleAniList = new SimpleAniListService();
        var user = simpleAniList.SearchUserAsync("nupniichan");
        Console.WriteLine(user.Result.avatar.medium);
    }
}
