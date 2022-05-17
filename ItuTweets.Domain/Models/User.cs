namespace ItuTweets.Domain.Models
{
    public class User
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public PublicMetrics Public_metrics { get; set; }
        public string? Username { get; set; }
    }
}