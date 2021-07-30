using System.Text.Json.Serialization;

namespace KanaTester
{
    public class Symbol 
    {
        public string JapaneseSymbol { get; set; }
        public string RomanizedSymbol { get; set; }
        public GuessAttempt[] Attempts { get; set; } = new GuessAttempt[10];
        public int TimesGuessedRight { get; set; }
        public int TimesGuessedWrong { get; set; }
        [JsonIgnore]
        public int TimesGuessed => TimesGuessedRight + TimesGuessedWrong;

        public void AddAttempt(GuessAttempt guess)
        {
            if (guess.IsSuccessful)
            {
                TimesGuessedRight++;
            } else
            {
                TimesGuessedWrong++;
            }
            ShiftAttempts(guess);
        }

        private void ShiftAttempts(GuessAttempt guessAttempt)
        {
            for (var i = Attempts.Length - 1; i > 0; i--)
            {
                Attempts[i] = Attempts[i - 1];
            }

            Attempts[0] = guessAttempt;
        }

        public override string ToString()
        {
            return $"Option: \n" +
                   $"  -  {nameof(JapaneseSymbol)}: {JapaneseSymbol}, \n" +
                   $"  -  {nameof(RomanizedSymbol)}: {RomanizedSymbol}, \n" +
                   $"  -  {nameof(TimesGuessedRight)}: {TimesGuessedRight}, \n" +
                   $"  -  {nameof(TimesGuessedWrong)}: {TimesGuessedWrong}, \n" +
                   $"  -  {nameof(TimesGuessed)}: {TimesGuessed}";
        }
    }
}