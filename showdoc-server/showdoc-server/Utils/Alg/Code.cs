using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace showdoc_server.Utils
{
    public class Code
    {
        public static string GetCode()
        {
            List<int> list = new List<int>();

            Enumerable.Range(97, 26).Map(r => list.Add(Convert.ToChar(r)));

            Enumerable.Range(65, 26).Map(r => list.Add(Convert.ToChar(r)));

            Enumerable.Range(0, 10).Map(r => list.Add(r));

            IEnumerable<int> raffledList = Raffle(list);

            StringBuilder builder = new StringBuilder();
            raffledList.Map(r => builder.Append(r.ToString()));

            return builder.ToString().Substring(0, 6);
        }

        public static IEnumerable<T> Raffle<T>(IEnumerable<T> input)
        {
            System.Random rd = new System.Random(DateTime.Now.Millisecond);
            T[] copyInput = new T[input.Count()];

            input.ToList().CopyTo(copyInput);

            List<T> copyList = copyInput.ToList();

            List<T> outputList = new List<T>();

            while (copyList.Count() > 0)
            {
                int rdIndex = rd.Next(0, copyList.Count - 1 < 0 ? 0 : copyList.Count - 1);
                T tempOutputItem = copyList[rdIndex];

                copyList.Remove(tempOutputItem);
                outputList.Add(tempOutputItem);
            }

            return outputList;
        }
    }
}
