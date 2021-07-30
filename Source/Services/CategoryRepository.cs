using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KanaTester
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly HttpClient _http;
        private IList<Category> _categories = null;

        public CategoryRepository(HttpClient http)
        {
            _http = http;
        }

        private async Task<IList<Category>> GetCategories()
        {
            if (_categories is not null)
            {
                return _categories;
            }

            _categories = await _http.GetFromJsonAsync<IList<Category>>(LocalStorageNames.FileNames[LocalStorageNames.Categories]);
            return _categories;
        }

        public async Task<IList<string>> GetSymbolsInCategory(string categoryName)
        {
            var cat = (await GetCategories())
                .First(x => x.Name == categoryName);

            List<string> symbols = cat
                .JapaneseSymbols
                .ToList();
            
            return symbols;
        }

        public async Task<IList<string>> GetCategoryNames() =>
            (await GetCategories()).Select(x => x.Name).ToList();
    }
}