using System;
using System.Linq;

namespace ConsoleAppSummator
{

    public class Summator
    {


        static void Main(string[] args)
        {
            Console.WriteLine($"Sum: {Sum(new int[] { 10, 20, 30 })}");
            Console.WriteLine($"Average: {Average(new int[] { 5, 20, 30 })}");
            Console.WriteLine($"Max number: {Max(new int[] { 100, 500, 300, 59, 89, 32, 47, 1, 6, 9 })}");
            Console.WriteLine($"Min number: {Min(new int[] { 5, 69, 486, 84, 99, 105, 624, 7 })}");
            Console.WriteLine($"First element: {FirstElement(new int[] { 100, 59, 84, 75 })}");
            Console.WriteLine($"Last element: {LastElement(new int[] {267,85,965,74,22 })}");
        }
        public static long Sum(int[] arr)
        {
            return arr.Sum();
        }
        public static double Average(int[] arr)
        {
            return arr.Average();

        }
        public static int Max(int[] arr)
        {
            return arr.Max();
        }
        public static int Min(int[] arr)
        {
            return arr.Min();
        }
        public static int FirstElement(int[] arr)
        {
            return arr.FirstOrDefault();
        }
        public static int LastElement(int[] arr)
        {
            return arr.LastOrDefault();
        }
    }
}
