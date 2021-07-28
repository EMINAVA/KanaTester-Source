namespace KanaTester
{
    public class Symbol 
    {
        public string JapaneseSymbol { get; set; }
        public string RomanizedSymbol { get; set; }
        public GuessAttempt[] Attempts { get; set; }
        public int TimesGuessedRight { get; set; }
        public int TimesGuessedWrong { get; set; }
    }
}