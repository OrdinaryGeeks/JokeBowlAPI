namespace JokeAIAPI.Models
{
    public class Setup
    {
        public int SetupID { get; set; }
        public int JokeID { get; set; }
        public int Order { get; set; }

        public required string Text { get; set; }
    }
}
