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

        public List<PostsByHourRequest> GetPostsByHour()
        {
            var result = _context.Tweets
                .AsEnumerable()
                .GroupBy(x => new
                {
                    x.Created_at.Date,
                    x.Created_at.Hour
                })
                .Select(x => new
                {
                    x.Key.Date,
                    Hour = x.Key.Hour - 3,
                    Count = x.Count()
                })
                .OrderByDescending(x => x.Date)
                .ThenByDescending(x => x.Hour);

            List<PostsByHourRequest> postsByHour = new();
            foreach (var item in result)
            {
                postsByHour.Add(new PostsByHourRequest
                {
                    Datetime = ($"{item.Date:dd/MM/yyyy} {item.Hour}:00:00"),
                    Total_posts = item.Count
                });
            }

            return postsByHour;
        }

        public List<PostByTagRequest> GetPostsByTag(string tag)
        {
            if (tag.Contains('#'))
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
