namespace JokeAIAPI.Models
{
    public class Joke
    {
        public int JokeID { get; set; }
        public int JokeTypeID { get; set; }
        public int CategoryID { get; set; }
        public int Score { get; set; } 
        public required string JokeName { get; set; }
        public required string UserName { get; set; }

        public required string Text { get; set; }
    }
}
