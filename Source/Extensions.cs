using System;
using System.Collections.Generic;

namespace KanaTester
{
    public static class Extensions
    {
        private static readonly Random _random = new(DateTime.Now.Millisecond);
        public static T Random<T>(this IList<T> collection)
        {
            var i = _random.Next(collection.Count);
            return collection[i];
        }
    }
}