using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace KanaTester
{
    public class SymbolRepositoryFactory : ISymbolRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SymbolRepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<ISymbolRepository> CreateKatakanaSymbolRepository()
        {
            var rep =  ActivatorUtilities.CreateInstance<SymbolRepository>(_serviceProvider, 
                LocalStorageNames.Katakana);
            await rep.Init();
            return rep;
        }

        public async Task<ISymbolRepository> CreateHiraganaSymbolRepository()
        {
            var rep = ActivatorUtilities.CreateInstance<SymbolRepository>(_serviceProvider, 
                LocalStorageNames.Hiragana);
            await rep.Init();
            return rep;
        }
    }
}