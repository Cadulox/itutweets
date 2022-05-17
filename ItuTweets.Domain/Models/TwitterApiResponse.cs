namespace ItuTweets.Domain.Models
{
    public class TwitterApiResponse
    {
        public List<Tweet> Data { get; set; }
        public Includes Includes { get; set; }
    }
}
