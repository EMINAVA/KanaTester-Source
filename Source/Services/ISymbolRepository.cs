

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KanaTester 
{
    public interface ISymbolRepository : IDisposable, IAsyncDisposable
    {
        Task Init();
        IList<Symbol> GetSymbols();
        Symbol GetSymbolByJapanese(string japaneseSymbol);
        Symbol GetSymbolByRomaji(string romajiSymbol);
        void UpdateSymbol(Symbol symbol);
        void SaveChanges();
    }
}