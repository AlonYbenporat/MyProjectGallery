using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace LinqDemo
{
    public static class StringExtensions
    {
        public static string FirstLetterToupper(this string input)
        {
            if(string.IsNullOrEmpty(input))
            { 
                return input;
            }
            return  char.ToUpper(input[0]) + input.Substring(1);
        }
        public static string ReturnAWord<T>(this IEnumerable<T> input)
        {
            return "Word!";
        }

        
    }
    public static class IntExtensions
    {
        public static bool IsEven(this int input)
        {
            return input % 2 == 0;
        }

    }
}
