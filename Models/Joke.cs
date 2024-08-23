namespace JokeAIAPI.Models
{
    public class Joke
    {
        public int JokeID { get; set; }
        public required string JokeType { get; set; }
        public required string Category { get; set; }
        public int Score { get; set; } 
        public required string JokeName { get; set; }
        public required string UserName { get; set; }

        public required string Text { get; set; }
        public required string PunchLine { get; set; }
        public required string Setup { get; set; }
        public required string Subject { get; set; }
    }
}
