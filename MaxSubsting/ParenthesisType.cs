using System;
using System.Collections.Generic;

namespace ExtensionMethods
{
    public static class ParenthesisType
    {
        private static List<char[]> _arrayList = new List<char[]>();

        static ParenthesisType()
        {
            char[] leftParentheses = { '{', '(', '[' };
            char[] rightParentheses = { '}', ')', ']' };
            _arrayList.Add(leftParentheses);
            _arrayList.Add(rightParentheses);
        }

        
        // возвращаемые параметры кортеж --> (вид скобки, открывающая или закрывающая)
        public static (int, int) GetType(char ch)
        {
            int type = -1;
            int openClose = 0;

            foreach(var arr in _arrayList) 
            {
                openClose++;
                type = Array.IndexOf(arr, ch);
                if (type != -1) break; 
            }

            if (type == -1) openClose = 0;

            return (type, openClose);
        }
    }

  

} // end of namespace
