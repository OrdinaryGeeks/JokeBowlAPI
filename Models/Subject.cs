namespace JokeAIAPI.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public int JokeID { get; set; }
        public int Order { get; set; }

        public required string Text { get; set; }

        public Subject() {  }
    }
}
