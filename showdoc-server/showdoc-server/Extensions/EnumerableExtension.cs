using System;
using System.Collections.Generic;

namespace System.Linq
{
    public static class ArrayExtension
    {
        public static void Map<T>(this IEnumerable<T> list, Action<T> iteractionFunc)
        {
            foreach (var item in list)
            {
                iteractionFunc(item);
            }
        }
    }
}
