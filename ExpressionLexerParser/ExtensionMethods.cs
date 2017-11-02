using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionLexerParser
{
    public static class ExtensionMethods
    {
        public static void CombineStack<T>(this Stack<T> stack1, Stack<T> stack2)
        {
            T[] arr = new T[stack2.Count];
            stack2.CopyTo(arr, 0);

            for(var i = 0; i < arr.Length; i++)
                stack1.Push(arr[i]);
        }
    }
}
