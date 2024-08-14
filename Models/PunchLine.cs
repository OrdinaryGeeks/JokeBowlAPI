namespace JokeAIAPI.Models
{
    public class PunchLine
    {
        public int PunchLineID { get; set; }
        public int JokeID { get; set; }
        public int Order { get; set; }
        public required string Text { get; set; }
    }
}
