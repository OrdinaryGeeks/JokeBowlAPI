namespace JokeAIAPI.Models
{
    public class JokeDownVote
    {
        public int JokeDownVoteID { get; set; }
        public int JokeID { get; set; }
        public required string UserName { get; set; }

    }
}
