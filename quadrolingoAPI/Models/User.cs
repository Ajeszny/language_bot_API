namespace quadrolingoAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public long CHAT_ID { get; set; }
        public long TELEGRAM_ID {  get; set; }
        public string USERNAME { get; set; }
        public string FIRST_NAME { get; set; }
        public string? LAST_NAME { get; set;}
        public string BASE_LANG { get; set; }
        public string STUDY_LANG { get; set;}
    }
}
