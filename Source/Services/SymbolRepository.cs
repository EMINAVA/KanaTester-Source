using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;

namespace KanaTester 
{
    public class SymbolRepository : ISymbolRepository
    {
        private readonly ISyncLocalStorageService _localStorageService;
        private readonly HttpClient _http;
        private readonly ISerializingService _serializingService;
        private List<Symbol> _symbols = new();
        private readonly ICategoryRepository _categoryRepository;
        private readonly string _symbolCategory;
        private readonly ILogger<SymbolRepository> _logger;

        public SymbolRepository(ISyncLocalStorageService localStorageService, HttpClient http, 
            ISerializingService serializingService, ICategoryRepository categoryRepository, 
            string symbolCategory, ILogger<SymbolRepository> logger)
        {
            _localStorageService = localStorageService;
            _http = http;
            _serializingService = serializingService;
            _categoryRepository = categoryRepository;
            _symbolCategory = symbolCategory;
            _logger = logger;
        }

        public async Task Init()
        {
            if (!_localStorageService.ContainKey(_symbolCategory)) 
            {
                _symbols = await GetSymbolsFromJson();
                SaveChanges();
            } else
            {
                var symbolsString = _localStorageService.GetItem<string>(_symbolCategory);
                _symbols = _serializingService.Deserialize<List<Symbol>>(symbolsString);
            }
        }

        private async Task<List<Symbol>> GetSymbolsFromJson()
        {
            var str = await _http.GetStringAsync(LocalStorageNames.FileNames[_symbolCategory]);
            return _serializingService.Deserialize<List<Symbol>>(str);
        }

        public IList<Symbol> GetSymbols() 
        {
            var symbols = _localStorageService.GetItem<string>(_symbolCategory);
            return _serializingService.Deserialize<IList<Symbol>>(symbols);
        }

        public Symbol GetSymbolByJapanese(string japaneseSymbol)
        {
            return _symbols.First(x => x.JapaneseSymbol == japaneseSymbol);
        }
        
        public Symbol GetSymbolByRomaji(string romajiSymbol)
        {
            _logger.LogInformation(romajiSymbol);
            return _symbols.First(x => x.RomanizedSymbol == romajiSymbol);
        }
        
        public void UpdateSymbol(Symbol symbol) 
        {
            var index = _symbols.FindIndex(x => x.JapaneseSymbol == symbol.JapaneseSymbol);
            _symbols[index] = symbol;
        }

        public void SaveChanges()
        {
            Console.WriteLine("Changes saved");
            var serializedSymbols = _serializingService.Serialize(_symbols);
            _localStorageService.SetItem(_symbolCategory, serializedSymbols);
        }

        public async Task<IList<Symbol>> GetSymbolsByCategory(List<string> categories)
        {
            var symbols = new List<Symbol>();
            foreach (var category in categories)
            {
                foreach (var symbol in await _categoryRepository.GetSymbolsInCategory(category))
                {
                    symbols.Add(GetSymbolByRomaji(symbol));
                }
            }
            return symbols;
        }

        public void Dispose()
        {
            SaveChanges();
        }

        public ValueTask DisposeAsync()
        {
            SaveChanges();
            return ValueTask.CompletedTask;
        }
    }
}