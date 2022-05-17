using ItuTweets.Domain.RequestModels;

namespace ItuTweets.Services.Interfaces
{
    public interface ITweetDataService
    {
        List<TwitterUserRequest> GetTopFiveUsers();
        List<PostByTagRequest> GetPostsByTag(string tag);
    }
}
