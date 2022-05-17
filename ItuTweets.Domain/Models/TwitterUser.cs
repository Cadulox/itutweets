namespace ItuTweets.Domain.Models
{
    public class TwitterUser
    {
        public TwitterUser()
        {
            Uuid = Guid.NewGuid().ToString();
        }
        public string Uuid { get; private set; }
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public int TweetCount { get; set; }
        public int ListedCount { get; set; }
    }
}
