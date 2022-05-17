namespace ItuTweets.Domain.RequestModels
{
    public class TwitterUserRequest
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public int Tweets { get; set; }
    }
}
