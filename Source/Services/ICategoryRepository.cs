using System.Collections.Generic;
using System.Threading.Tasks;

namespace KanaTester
{
    public interface ICategoryRepository
    {
        Task<IList<Symbol>> GetSymbolsInCategory(string categoryName);
        Task<IList<string>> GetCategoryNames();
    }
}