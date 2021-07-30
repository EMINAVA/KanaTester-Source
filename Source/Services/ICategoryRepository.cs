using System.Collections.Generic;
using System.Threading.Tasks;

namespace KanaTester
{
    public interface ICategoryRepository
    {
        Task<IList<string>> GetSymbolsInCategory(string categoryName);
        Task<IList<string>> GetCategoryNames();
    }
}