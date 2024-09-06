namespace quadrolingoAPI.Models
{
    public class UserWord
    {
        public int Id { get; set; }
        public DateTime TIMESTAMP { get; set; }
        public Word WORD_ID { get; set; }
        public User USER_ID { get; set; }
    }
}
