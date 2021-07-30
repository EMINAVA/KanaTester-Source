using System.Collections.Generic;
using System.Threading.Tasks;

namespace KanaTester
{
    public interface ISymbolPicker
    {
        Task<Symbol> PickSymbol(List<string> categories, ISymbolRepository symbolRepository);
    }
}