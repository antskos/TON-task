using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static bool IsBalancedParenthesis(this string str)
        {
            Stack<char> stk = new Stack<char>();
            
            foreach (char ch in str.ToCharArray())
            {
                switch (ch)
                {
                    case '{':
                    case '[':
                    case '(':
                        stk.Push(ch);
                        break;

                    case '}':
                        if(stk.Count == 0)
                            return false;
                        if (stk.Pop() != '{')
                            return false;
                        break;

                    case ']':
                        if (stk.Count == 0)
                            return false;
                        if (stk.Pop() != '[')
                            return false;
                        break;

                    case ')':
                        if (stk.Count == 0)
                            return false;
                        if (stk.Pop() != '(')
                            return false;
                        break;
                }
            }
            return stk.Count == 0;
        }

        public static bool IsInverseBalancedParenthesis(this string str)
        {
            Stack<char> stk = new Stack<char>();

            foreach (char ch in str.ToCharArray())
            {
                switch (ch)
                {
                    case '}':
                    case ']':
                    case ')':
                        stk.Push(ch);
                        break;

                    case '{':
                        if (stk.Count == 0)
                            return false;
                        if (stk.Pop() != '}')
                            return false;
                        break;

                    case '[':
                        if (stk.Count == 0)
                            return false;
                        if (stk.Pop() != ']')
                            return false;
                        break;

                    case '(':
                        if (stk.Count == 0)
                            return false;
                        if (stk.Pop() != ')')
                            return false;
                        break;
                }
            }
            return stk.Count == 0;
        }

        public static string FindMaxBalancedSubs(this string str)
        {
            int beginSubsPosition = 0;       // хранит позицию начала максимальной посдтроки (hold start position of max substring)
            int endSubsPosition = 0;         // хранит позиции конца подходящих посдтрок (hold end position of max substring)
            
            var parenthesis = (int position, char symbolType);		// кортеж позиции скобки в строке и её типа

            Stack<char> leftParenStk = new Stack<char>();      // хранит типы левых скобок (hold the left parenthesis types)
            Stack<char> rightParenStk = new Stack<char>();    // хранит типы правых скобок (hold the right parenthesis types)

            Stack<int> leftPos = new Stack<int>();
            Stack<int> rightPos = new Stack<int>();

            leftPos.Push(-1);
            rightPos.Push(-1);

            int currentMaximum = 0;
            //int globalMaximum = 0;
            
            char[] leftParenArray = { '{', '(', '['};
            char[] rightParenArray = { '}', ')', ']'};

            leftPos.Push(str.IndexOfAny(leftParenArray));
            rightPos.Push(str.IndexOfAny(rightParenArray));

            if(leftPos.Peek() > 0 && rightPos.Peek() > 0)     // включение начала строки
            {
                currentMaximum = Math.Min(leftPos.Peek(), rightPos.Peek());
                beginSubsPosition.Push(0);
                endSubsPosition.Push(currentMaximum - 1);
                //globalMaximum = currentMaximum;
            }

            if (leftPos.Peek() < rightPos.Peek()) // если первая открывающая скобка левее всех закрывающих
            {
                while (leftPos.Peek() < rightPos.Peek())        // заполняем стэк левыми скобками до правой
                {
                    int index = str.IndexOfAny(leftParenArray, leftPos.Peek() + 1, rightPos.Peek() - (leftPos.Peek() + 1));
                    if (index == -1) break;
                    else leftPos.Push(index);
                }

                if (Array.IndexOf(leftParenArray, str[leftPos.Peek()]) == Array.IndexOf(rightParenArray, str[rightPos.Peek()]))        // если тип скобок относится к одной группе, то ПСП
                {
                    if (rightPos.Peek() - leftPos.Peek() > currentMaximum)
                    {
                        currentMaximum = rightPos.Peek() - leftPos.Peek();
                        //globalMaximum = Math.Max(currentMaximum, globalMaximum);
                    }

                    if (leftParenStk.Count != 0) leftParenStk.Pop();
                    if (rightParenStk.Count != 0) rightParenStk.Pop();
                    if (endSubsPosition.Count != 0) endSubsPosition.Pop();
                    endSubsPosition.Push(rightPos);
                }

                else // не ПСП
                {

                }

            }
                

        }
                


                    
          
    } // end of ExtensionMethods class
} // end of namespace
