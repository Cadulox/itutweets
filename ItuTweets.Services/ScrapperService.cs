using ItuTweets.Data.Context;
using ItuTweets.Domain.Models;
using ItuTweets.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ItuTweets.Services
{
    public class ScrapperService : IScrapperService
    {
        private const string BEARER_TOKEN = "AAAAAAAAAAAAAAAAAAAAAAtbcgEAAAAAEs4KL5u47HPe5iqidGVSeQOaw2I%3DPelQ0LaGVD2OUDQEl7cl09HEabP5bf8g98sVonmJBrwOSc2ROW";
        private readonly List<string> _hashtags = new() { "openbanking", "remediation", "devops", "sre", "microservices", "observability", "oauth", "metrics", "logmonitoring", "opentracing" };
        private readonly ItuTweetsContext _context;

        public ScrapperService(ItuTweetsContext context)
        {
            _context = context;
        }

        public void FillDatabase()
        {
            _context.Database.ExecuteSqlRaw("DELETE FROM tb_tweets");
            _context.Database.ExecuteSqlRaw("DELETE FROM tb_twitter_users");
            Console.WriteLine("Database clear");

            foreach (var hash in _hashtags)
            {
                RequestTwitterAPI(hash);
            }
        }

        protected void RequestTwitterAPI(string hashtag)
        {
            var uriApi = $"https://api.twitter.com/2/tweets/search/recent?query=%23{hashtag}&expansions=author_id&user.fields=public_metrics&tweet.fields=text,author_id,created_at,lang&max_results=100";
            HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BEARER_TOKEN);
            var response = httpClient.GetStringAsync(uriApi);
            response.Wait();
            var result = JsonConvert.DeserializeObject<TwitterApiResponse>(response.Result);

            if (result == null || result.Data != null && result.Includes != null)
            {
                var tweets = result.Data;
                var users = result.Includes.Users;

                foreach (var tweet in tweets)
                {
                    var existing = _context.Tweets.FirstOrDefault(e => e.Id == tweet.Id);
                    if (existing == null)
                    {
                        _context.Tweets.Add(tweet);
                    }
                }

                foreach (var user in users)
                {
                    var existing = _context.TwitterUsers.FirstOrDefault(e => e.Id == user.Id);
                    if (existing == null)
                    {
                        var twitterUser = new TwitterUser
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Username = user.Username,
                            FollowersCount = user.Public_metrics.Followers_count,
                            FollowingCount = user.Public_metrics.Following_count,
                            ListedCount = user.Public_metrics.Listed_count,
                            TweetCount = user.Public_metrics.Tweet_count
                        };
                        _context.TwitterUsers.Add(twitterUser);
                    }
                }
                _context.SaveChanges();
            }
        }
    }
}