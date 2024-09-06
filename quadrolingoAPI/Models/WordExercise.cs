namespace quadrolingoAPI.Models
{
    public class WordExercise
    {
        public int Id { get; set; }
        public Word WORD_ID { get; set; }
        public Exercise EXERCISE_ID { get; set; }
        public Boolean Guessed { get; set; }
    }
}
