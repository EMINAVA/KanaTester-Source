using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace KanaTester 
{
    public class SymbolRepository : ISymbolRepository
    {
        private readonly ISyncLocalStorageService _localStorageService;
        private readonly HttpClient _http;
        private List<Symbol> _symbols = new();

        public SymbolRepository(ISyncLocalStorageService localStorageService, HttpClient http)
        {
            Console.WriteLine("Symbol rep created");
            _localStorageService = localStorageService;
            _http = http;
        }

        public async Task Init()
        {
            if (!_localStorageService.ContainKey(LocalStorageNames.Symbols) /*TODO: REMOVE*/ || true) 
            {
                _symbols = await GetSymbolsFromJson();
                _localStorageService.SetItem(LocalStorageNames.Symbols, _symbols);
            } else
            {
                _symbols = _localStorageService.GetItem<List<Symbol>>(LocalStorageNames.Symbols);
            }
        }

        private Task<List<Symbol>> GetSymbolsFromJson() 
        {
            return _http.GetFromJsonAsync<List<Symbol>>("default-symbols.json");
        }

        public IList<Symbol> GetSymbols() 
        {
            return _localStorageService.GetItem<IList<Symbol>>(LocalStorageNames.Symbols);
        }

        public Symbol GetSymbolByJapanese(string japaneseSymbol)
        {
            return _symbols.First(x => x.JapaneseSymbol == japaneseSymbol);
        }
        
        public Symbol GetSymbolByRomaji(string romajiSymbol)
        {
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
            _localStorageService.SetItem(LocalStorageNames.Symbols, _symbols);
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