namespace ItuTweets.Domain.RequestModels
{
    public class PostByTagRequest
    {
        public string Tag { get; set; }
        public string Language { get; set; }
        public int Total_posts { get; set; }
    }
}
