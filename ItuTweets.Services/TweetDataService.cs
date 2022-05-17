using ItuTweets.Data.Context;
using ItuTweets.Domain.RequestModels;
using ItuTweets.Services.Interfaces;

namespace ItuTweets.Services
{
    public class TweetDataService : ITweetDataService
    {
        private readonly ItuTweetsContext _context;
        public TweetDataService(ItuTweetsContext context)
        {
            _context = context;
        }

        public List<TwitterUserRequest> GetTopFiveUsers()
        {
            var users = _context.TwitterUsers
                .OrderByDescending(x => x.FollowersCount)
                .Take(5);
            List<TwitterUserRequest> twitterUsers = new();
            foreach (var user in users)
            {
                twitterUsers.Add(new TwitterUserRequest
                {
                    Id = user.Id,
                    Name = user.Name,
                    Username = user.Username,
                    Followers = user.FollowersCount,
                    Following = user.FollowingCount,
                    Tweets = user.TweetCount
                });
            }
            return twitterUsers;
        }

        public List<PostByTagRequest> GetPostsByTag(string tag)
        {
            if (tag.Contains("#"))
            {
                tag = tag.Replace("#", String.Empty);
            }
            var postsByTag =
                from tweets in _context.Tweets
                where tweets.Text.Contains(tag)
                group tweets by tweets.Lang into tweetsGroup
                select new PostByTagRequest
                {
                    Tag = tag,
                    Language = tweetsGroup.Key,
                    Total_posts = tweetsGroup.Count(),
                };

            return postsByTag.ToList();
        }
    }
}
