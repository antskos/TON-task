using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using ExtensionMethods;     

namespace MaximalSubstringFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            string myStr = "wefвыаыа([{sfdsf}}])фыврл!*?:";

            Console.WriteLine(FilterString(myStr));
            string local = FilterString(myStr);

            string str1;
            double myResult = FindMaxSubstring(local, out str1);
            Console.WriteLine(myResult);
            Console.WriteLine(str1);
        }


        // оставляю в строке только нужные символы
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

        public static double FindMaxSubstring(string str, out string str1)
        {
            int result = str.Where(ch => Char.IsLetter(ch)).Count();        // подсчёт в s кол-ва символов отличных от скобок

            str1 = new string(str.Where(ch => !Char.IsLetter(ch)).ToArray());       // выделяю строчку только со скобками, для дальнейшей работы

            if (str1.IsBalancedParenthesis() ||         // balanced
                str1.IsInverseBalancedParenthesis())
                return double.PositiveInfinity;
            else;                                       // unbalanced

            return result;
        }
    }
}
