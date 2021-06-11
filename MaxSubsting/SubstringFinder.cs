using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using ExtensionMethods;     

namespace SubstringFinder
{
    public class SubstringFinder
    {
        static void Main(string[] args)
        {
            // examples from task
            string str1 = "}](){";
            string str2 = "sh(dh)}";
            string str3 = "]h({hdb}b)[";

            double subLength = 0;

            Console.OutputEncoding = System.Text.Encoding.Unicode;      // for the infinity symbol
            Console.WriteLine($"Максимальная подстрока строки str1: {FindMaxSubstring(str1, out subLength)}. Длина подстроки: {subLength}");
            Console.WriteLine($"Максимальная подстрока строки str1: {FindMaxSubstring(str2, out subLength)}. Длина подстроки: {subLength}");
            Console.WriteLine($"Максимальная подстрока строки str1: {FindMaxSubstring(str3, out subLength)}. Длина подстроки: {subLength}");
        }

        /// <summary>
        /// Combines ranges that have an intersections or closer to each other than 2 units
        /// Объединяет пересекающиеся или рядом лежащие интервалы в один эквивалентный 
        /// </summary>
        /// <param name="list">Collection of ranges in the original string</param>
        /// <returns>IEnumerable<(int, int)> interface</returns>
        public static IEnumerable<(int, int)> MaximazeSubstrings(List<(int, int)> list)
        {
            if (list.Count == 0) yield break;

            list.Sort();

            var local = list[0];
            if (list.Count == 1) yield return local;

            foreach (var item in list.Skip(1))
            {
                if (item.Item1 >= local.Item1 && item.Item1 - 1 <= local.Item2)
                {
                    local.Item1 = Math.Min(local.Item1, item.Item1);
                    local.Item2 = Math.Max(local.Item2, item.Item2);
                }
                else
                {
                    yield return local;
                    local = item;
                }
                if (item == list.Last()) yield return local;
            }
        }

        // Method leaves only appropriate characters.
        // Метод фильтрует строку, оставляя в ней только подходящие символы.
        public static string FilterString(string str) 
        {
            StringBuilder sb = new StringBuilder();
            foreach(char ch in str) 
            {
                if ((ch >= 65 && ch <= (65 + 25)) ||        // заглавные
                    (ch >= 97 && ch <= (97 + 25))) sb.Append(ch);       // строчные
                else if (ch == '{' || ch == '}' || ch == '(' || ch == ')' || ch == '[' || ch == ']') sb.Append(ch);
                else continue;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Finds maximal CBS substring.
        /// </summary>
        /// <param name="str">original string</param>
        /// <param name="subLength">the output parameter: substring length.</param>
        /// <returns>Maximal CBS substring</returns>
        public static string FindMaxSubstring(string str, out double subLength)
        {
            string result;

            if (string.IsNullOrEmpty(str))
            {
                subLength = 0;
                return "Input is null or empty.";
            }

            string local = FilterString(str);
            if (string.IsNullOrEmpty(local))
            {
                subLength = 0;
                return "Input doesn't contain appropriate symbols.";
            }

            // line selection with brackets only (выделение строчки только со скобками, для пары функций)
            string strPar = new string(local.Where(ch => !Char.IsLetter(ch)).ToArray());

            if (string.IsNullOrEmpty(strPar)) 
            {
                subLength = double.PositiveInfinity;
                return "Infinite";
            }

            if (strPar.IsBalancedParenthesis())            // balanced case
            {
                subLength = double.PositiveInfinity;
                return "Infinite";
            }
            else                                       // unbalanced case
            {
                IEnumerable<(int, int)> modifiedList = MaximazeSubstrings(local.FindSubs());
                var maxLoopedSub = local.FindLoopedSub(ExtensionMethods.ExtensionMethods.FindNoCBSSubs(local, modifiedList.ToList()));    // maximal sub in cocatenated string through end of orig. str(наибольшая подстрока на границе двух строк)

                if (modifiedList.Any())
                {
                    var maxSub = modifiedList.OrderByDescending(x => x.Item2 - x.Item1).First();       // maximal subs in orig. not concatenated string(наибольшая подстрока в одиночной строке)
                    int maxSub_Length = maxSub.Item2 - maxSub.Item1 + 1;

                    result = maxSub_Length >= maxLoopedSub.Length ?
                             local.Substring(maxSub.Item1, maxSub_Length) : maxLoopedSub;
                }
                else result = maxLoopedSub;
            }

            subLength = result.Length;
            return result;
        }
    }
}
