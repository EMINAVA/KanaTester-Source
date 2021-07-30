namespace KanaTester
{
    public class Guesser : IGuesser
    {
        public bool Guess(Symbol symbol, string guess)
        {
            if (guess.Trim().ToLower() == symbol.RomanizedSymbol)
            {
                symbol.AddAttempt(new GuessAttempt
                {
                    IsSuccessful = true,
                    JapaneseSymbol = symbol.JapaneseSymbol
                });
                return true;
            }
            
            symbol.AddAttempt(new GuessAttempt
            {
                IsSuccessful = false,
                JapaneseSymbol = symbol.JapaneseSymbol,
                WrongGuess = guess
            });
            return false;
        }
    }
}