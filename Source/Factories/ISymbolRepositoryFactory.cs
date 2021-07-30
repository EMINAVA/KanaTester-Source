using System.Threading.Tasks;

namespace KanaTester
{
    public interface ISymbolRepositoryFactory
    {
        Task<ISymbolRepository> CreateKatakanaSymbolRepository();
        Task<ISymbolRepository> CreateHiraganaSymbolRepository();
    }
}