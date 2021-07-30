using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace KanaTester
{
    public class SymbolPicker : ISymbolPicker
    {
        private readonly ILogger<SymbolPicker> _logger;

        public SymbolPicker(ILogger<SymbolPicker> logger)
        {
            _logger = logger;
        }

        public async Task<Symbol> PickSymbol(List<string> categories, ISymbolRepository symbolRepository)
        {
            IList<Symbol> symbols = await symbolRepository.GetSymbolsByCategory(categories);
            var options = new List<Option>();
            foreach (var symbol in symbols)
            {
                if (symbol.TimesGuessed == 0)
                {
                    options.Add(new Option { Value = symbol, Weight = 1f });
                    continue;
                }
                var right = symbol.Attempts.Count(x => x?.IsSuccessful ?? false);
                var wrong = symbol.Attempts.Count(x => !x?.IsSuccessful ?? false);
                options.Add(new Option { Value = symbol, Weight = 0.5f + (float) wrong / (right + wrong) * 0.5f });
            }
            
            _logger.LogInformation("Options: {@Options}", options);

            return RandomOption(options);
        }

        private Symbol RandomOption(List<Option> options)
        {
            var total = options.Sum(x => x.Weight);
            var randomNumber = new Random(DateTime.Now.Millisecond).NextDouble() * total;
            foreach (var option in options)
            {
                if (randomNumber < option.Weight)
                {
                    return option.Value;
                }

                randomNumber = randomNumber - option.Weight;
            }
            
            _logger.LogWarning("Algorithm probably failed");
            return options.Last().Value;
        }

        private class Option
        {
            public Symbol Value { get; set; }
            public float Weight { get; set; }

            public override string ToString()
            {
                return $"Value: {Value}\nWeight: {Weight}\n";
            }
        }
    }
}