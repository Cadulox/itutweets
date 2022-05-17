using ItuTweets.Domain.Models;

namespace ItuTweets.Scrapper
{
    public interface ITweetScrapper
    {
        public Task<TwitterApiResponse> GetTweetsData(string hashtag);
        //public Task<string> GetTweetsData(string hashtag);
    }
}
