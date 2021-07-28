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
        private readonly ISymbolRepository _symbolRepository;

        public CategoryRepository(HttpClient http, ISymbolRepository symbolRepository)
        {
            _http = http;
            _symbolRepository = symbolRepository;
        }

        private async Task<IList<Category>> GetCategories()
        {
            if (_categories is not null)
            {
                return _categories;
            }

            _categories = await _http.GetFromJsonAsync<IList<Category>>("categories.json");
            return _categories;
        }

        public async Task<IList<Symbol>> GetSymbolsInCategory(string categoryName)
        {
            var cat = (await GetCategories())
                .First(x => x.Name == categoryName);

            var symbols = cat
                .JapaneseSymbols
                .Select(x => _symbolRepository.GetSymbolByJapanese(x))
                .ToList();
            
            return symbols;
        }

        public async Task<IList<string>> GetCategoryNames() =>
            (await GetCategories()).Select(x => x.Name).ToList();
    }
}