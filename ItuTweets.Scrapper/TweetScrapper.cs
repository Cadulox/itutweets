using Newtonsoft.Json;
using System.Net.Http.Headers;
using ItuTweets.Domain.Models;

namespace ItuTweets.Scrapper
{
    public class TweetScrapper : ITweetScrapper
    {
        private const string BEARER_TOKEN = "AAAAAAAAAAAAAAAAAAAAAAtbcgEAAAAAEs4KL5u47HPe5iqidGVSeQOaw2I%3DPelQ0LaGVD2OUDQEl7cl09HEabP5bf8g98sVonmJBrwOSc2ROW";
        private readonly List<string> _hashtags = new() { "openbanking", "remediation", "devops", "sre", "microservices", "observability", "oauth", "metrics", "logmonitoring", "opentracing" };

        public async Task<TwitterApiResponse> GetTweetsData(string hashtag)
        {
            HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BEARER_TOKEN);
            var response = await httpClient.GetAsync($"https://api.twitter.com/2/tweets/search/recent?query=%23sre&expansions=author_id&user.fields=public_metrics&tweet.fields=text,author_id,created_at,lang&max_results=100");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TwitterApiResponse>(content);
            //var users = result.Includes.Users.ToList();
            //var tweets = result.Data.ToList();
            //List<PublicMetrics> metrics = new List<PublicMetrics>();
            //foreach (var user in users)
            //{
            //    var twitterUser = new TwitterUser
            //    {
            //        Id = user.Id,
            //        Name = user.Name,
            //        Username = user.Username,
            //        FollowersCount = user.Public_metrics.Followers_count,
            //        FollowingCount = user.Public_metrics.Following_count,
            //        TweetCount = user.Public_metrics.Tweet_count,
            //        ListedCount = user.Public_metrics.Listed_count
            //    };

            //    _context.TwitterUsers.Add(twitterUser);
            //    _context.SaveChanges();
            //}

            //_context.PublicMetrics.AddRange(metrics);
            //_context.Users.AddRange(users);
            //_context.Tweets.AddRange(tweets);
            //Console.WriteLine(tweets);
            //Console.WriteLine(users);
            //Console.WriteLine(metrics);
            //return result.ToString();
        }

    }
}