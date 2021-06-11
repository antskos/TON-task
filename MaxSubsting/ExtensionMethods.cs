using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Checks if the input string contains only English letters and correct sequences of brackets(CBS)
        /// given that the string can be looped.
        /// Метод проверяет содержит ли строка только английские буквы и правильные скобочные последовательности
        /// при условии, что исходная строка может быть закольцована.
        /// Метод возвращает является ли строка ПСП либо нет.
        /// </summary>
        /// <param name="str">original string</param>
        /// <returns>Returns true if an input string is balanced, else returns false</returns>
        public static bool IsBalancedParenthesis(this string str) 
        {
            int left = 0;
            int right = str.Length - 1;

            int typeL, typeR;       // тип скобок

            while (left < right) 
            {
                typeL = SearchOnTheLeft(str, ref left, right, out int leftStatus);
                if (leftStatus == 0) return false;
                if (leftStatus == 2) return true;

                typeR = SearchOnTheRight(str, ref right, left, out int rightStatus);
                if (rightStatus == 0) return false;
                if (rightStatus == 2) return true;

                if (typeL != typeR) return false;
                else 
                {
                    if (right - left == 1) return true;
                    left++;
                    right--;
                }
            }
            return false;
        }

        // "SearchOnTheLeft" method used in the "IsBalancedParenthesis" method
        // For finding the place there the string became not CBS starting from the left side
        public static int SearchOnTheLeft(string str, ref int pos, int border, out int status)
        {
            Stack<Parenthesis> stkPar = new Stack<Parenthesis>();
            status = 1;

            Parenthesis leftPar;

            while (true)
            {
                leftPar = new Parenthesis(str[pos], pos);

                if (leftPar.OpenClose == 1)
                    stkPar.Push(leftPar);
                else
                {
                    if (stkPar.Count != 0)
                    {
                        if (stkPar.Peek().Type == leftPar.Type)  // если тип закрывающей скобки совпал
                        {
                            stkPar.Pop();    // удаляю кусок ПСП
                        }
                        else
                        {
                            status = 0;
                            break;
                        }
                    }
                    else
                    {
                        status = 1;
                        break;
                    }
                }

                if (pos < border)
                {
                    pos++;
                    continue;
                }
                else
                {
                    if (stkPar.Count == 0)
                    {
                        status = 2;
                        break;
                    }
                    status = 0;
                    break;
                }
            }
            return leftPar.Type;
        }

        // "SearchOnTheRight" method used in the "IsBalancedParenthesis" method
        // For finding the place there the string became not CBS starting from the right side
        public static int SearchOnTheRight(string str, ref int pos, int border, out int status)
        {
            Stack<Parenthesis> stkPar = new Stack<Parenthesis>();
            status = 1;

            Parenthesis rightPar;

            while (true)
            {
                rightPar = new Parenthesis(str[pos], pos);

                if (rightPar.OpenClose == 2)
                    stkPar.Push(rightPar);
                else
                {
                    if (stkPar.Count != 0)
                    {
                        if (stkPar.Peek().Type == rightPar.Type)  // если тип открывающей скобки совпал
                        {
                            stkPar.Pop();    // удаляю кусок ПСП
                        }
                        else
                        {
                            status = 0;
                            break;
                        }
                    }
                    else
                    {
                        status = 1;
                        break;
                    }
                }

                if (pos > border)
                {
                    pos--;
                    continue;
                }
                else
                {
                    if (stkPar.Count == 0)
                    {
                        status = 2;
                        break;
                    }
                    status = 0;
                    break;
                }
            }
            return rightPar.Type;
        }

        /// <summary>
        /// Finds all the CBS for the case of a non-looped string 
        /// Находит все ПСП в исходной строке (этот метод не ищет закольцованные ПСП) 
        /// </summary>
        /// <param name="str">original string</param>
        /// <returns>All ranges of CBS via direct bypass order. Left int in the list element is a start range position,
        /// and right int is an end range position. Ranges are not sorted.</returns>
        public static List<(int, int)> FindSubs(this string str)
        {
            int leftBorder = 0;
            int rightBorder = 0;
            List<(int, int)> substrings = new List<(int, int)>();

            Stack<Parenthesis> myPar = new Stack<Parenthesis>();	

            char[] Parentheses = { '{', '(', '[', '}', ')', ']'};


            while (true) 
            {
                int parPos = str.IndexOfAny(Parentheses, leftBorder);

                if (parPos != -1)
                {
                    var par = ParenthesisType.GetType(str[parPos]);
                    int type = par.Item1;       // локальная переменная для определения типа скобки
                    int openClose = par.Item2;	// локальная переменная для определения открытости закрытости скобки

                    if (openClose == 1)
                        myPar.Push(new Parenthesis(str[parPos], parPos));

                    if (parPos - leftBorder >= 1)
                    {
                        rightBorder = parPos - 1;
                        substrings.Add((leftBorder, rightBorder));
                    }

                    if (openClose == 2)
                    {
                        if (myPar.Count != 0)
                        {
                            if (myPar.Peek().Type == type)  // если тип закрывающей скобки совпал
                            {
                                leftBorder = myPar.Peek().Position;
                                rightBorder = parPos;
                                substrings.Add((leftBorder, rightBorder));
                                myPar.Pop();    // после добавления подстроки в список удаляю кусок ПСП
                            }
                            else myPar.Clear();
                        }
                    }
                   
                    leftBorder = parPos + 1;
                    rightBorder = leftBorder;
                    continue;
                }
                else
                {
                    rightBorder = str.Length - 1;
                    if (rightBorder > leftBorder) substrings.Add((leftBorder, rightBorder));
                    break;
                }
            }
                       
            return substrings;
        }

        /// <summary>
        /// Find all the ranges where CBS rules violated.
        /// Находит все диапазоны символов в строке, в которых нарушены условия ПСП.
        /// </summary>
        /// <param name="str">original string</param>
        /// <param name="list">List of CBS ranges</param>
        /// <returns>All NON CBS ranges via direct bypass order. Left int in the list element is a start range position,
        /// and right int is an end range position. Ranges are sorted and have no any intersections.</returns>
        public static List<(int, int)> FindNoCBSSubs(this string str, List<(int, int)> list) 
        {
            List<(int, int)> result = new List<(int, int)>();

            int max = str.Length - 1;
            int left = 0;
            int right = max;

            if (list.Count == 0)
            {
                result.Add((-1, -1));
                return result;
            }
            else list.Sort();

            foreach (var range in list)
            {
                if (range.Item1 == 0 && range != list.Last())
                {
                    if (range.Item2 < max) left = range.Item2 + 1;
                    continue;
                }
                else 
                {
                    right = range.Item1 - 1;
                    result.Add((left, right));
                }

                if (range.Item2 < max) left = range.Item2 + 1;

                if (range == list.Last())
                {
                    if (range.Item2 < max)
                    {
                        left = range.Item2 + 1;
                        right = max;
                        result.Add((left, right));
                    }
                    break;
                }

            }

            return result;
        }

        /// <summary>
        /// Finds the maximal CBS looped substring in the original string.
        /// Находит максимально возможную ПСП для закольцованной строки 
        /// для случая перехода через границу конца строки.
        /// </summary>
        /// <param name="str">original string</param>
        /// <param name="list">List of NO CBS ranges</param>
        /// <returns>The maximal looped substring through end of string</returns>
        public static string FindLoopedSub(this string str, List<(int, int)> list)
        {
            if (list[0] == (-1, -1)) 
            {
                return string.Empty;                
            }

            // anonymous method 1
            Func<int, int, bool> CompareParenthesis = (posL, posR) =>
            {
                var parL = ParenthesisType.GetType(str[posL]);
                int typeL = parL.Item1;       // локальная переменная для определения типа скобки
                int openCloseL = parL.Item2;	// локальная переменная для определения открытости закрытости скобки

                var parR = ParenthesisType.GetType(str[posR]);
                int typeR = parR.Item1;       // локальная переменная для определения типа скобки
                int openCloseR = parR.Item2;	// локальная переменная для определения открытости закрытости скобки

                if (typeL != typeR) return false;
                else
                {
                    if (openCloseL == 2 && openCloseR == 1) return true;
                    else return false;
                }
            };

            List<(int, int)> localList = new List<(int, int)>(list);    // copy of original list

            // anonymous method 2
            Func<(int, int)> TakeNextRangeFromLeft = () =>
            {
                (int, int) range;

                if (localList.Count >= 1)
                {
                    range = localList[0];
                    localList.RemoveAt(0);
                }
                else return (-1, -1);

                return range;
            };

            // anonymous method 3
            Func<(int, int)> TakeNextRangeFromRight = () =>
            {
                (int, int) range;

                if (localList.Count >= 1)
                {
                    range = localList.Last();
                    localList.RemoveAt(localList.Count - 1);
                }
                else return (-1, -1);

                return range;
            };

            (int, int) leftRange = (0, 0);
            (int, int) rightRange = (0, 0);

            leftRange = TakeNextRangeFromLeft();
            if (leftRange != (-1, -1)) rightRange = TakeNextRangeFromRight();
            if (rightRange == (-1, -1)) rightRange = leftRange;
            
            int curLeft = leftRange.Item1;
            int curRight = rightRange.Item2;
            
            while (true)
            {
                if (curLeft == curRight)
                {
                    curLeft--;
                    curRight++;
                    break;
                }
                else
                {
                    if (CompareParenthesis(curLeft, curRight))
                    {
                        if(curLeft < leftRange.Item2) curLeft++;
                        else 
                        {
                            if (rightRange != (-1, -1))
                            {
                                leftRange = TakeNextRangeFromLeft();
                                if (leftRange != (-1, -1)) curLeft = leftRange.Item1;
                                else curLeft = curRight;
                            }
                            else curLeft++;
                        } 
                        if (curLeft != curRight)
                        {
                            if(curRight > rightRange.Item1) curRight--;
                            else 
                            {
                                if (leftRange != (-1, -1))
                                {
                                    rightRange = TakeNextRangeFromRight();
                                    if (rightRange != (-1, -1)) curRight = rightRange.Item2;
                                    else curRight = curLeft;
                                }
                                else curRight--;
                            }
                            continue;
                        }
                        else
                        {
                            curLeft--;
                            break;
                        }
                    }
                    else
                    {
                        curLeft--;
                        curRight++;
                        break;
                    }
                }
            }

            return str.Substring(curRight) + str.Substring(0, curLeft + 1);
        }



    } // end of ExtensionMethods class

} // end of namespace
