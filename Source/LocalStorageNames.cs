using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace KanaTester
{
    public static class LocalStorageNames
    {
        public const string Categories = "Categories";
        public const string Hiragana = "Hiragana";
        public const string Katakana = "Katakana";

        public static readonly Dictionary<string, string> FileNames = new()
        {
            { "Hiragana", "Hiragana.json" },
            { "Katakana", "Katakana.json" },
            { "Categories", "Categories.json" }
        };
    }
}