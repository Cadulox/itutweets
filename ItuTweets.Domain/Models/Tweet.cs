namespace ItuTweets.Domain.Models
{
    public class Tweet
    {
        public Tweet()
        {
            Uuid = Guid.NewGuid().ToString();
        }
        public string Uuid { get; private set; }
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime Created_at { get; set; }
        public string Lang { get; set; }
        public string Author_id { get; set; }
    }
}