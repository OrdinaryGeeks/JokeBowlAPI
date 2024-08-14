namespace JokeAIAPI.Models
{
    public class JokeUpVote
    {
        public int JokeUpVoteID { get; set; }
        public int JokeID { get; set; }
        public required string UserName { get; set; }
    }
}
