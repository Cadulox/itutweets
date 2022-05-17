namespace ItuTweets.Domain.RequestModels
{
    public class PostsByHourRequest
    {
        public string Datetime { get; set; }
        public int Total_posts { get; set; }
    }
}