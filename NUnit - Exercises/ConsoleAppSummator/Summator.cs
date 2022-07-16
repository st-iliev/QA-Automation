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
            Console.WriteLine($"Last element: {LastElement(new int[] { 267, 85, 965, 74, 22 })}");
        }
        public static long Sum(int[] arr)
        {
            long sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
            }
            return sum;
        }
        public static double Average(int[] arr)
        {
            double sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
            }
            return sum / arr.Length;

        }
        public static int Max(int[] arr)
        {
            int max = int.MinValue;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }
            return max;
        }
        public static int Min(int[] arr)
        {
            int min = int.MaxValue;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                }
            }
            return min;
        }
        public static int FirstElement(int[] arr)
        {
            int firstElement = arr[0];
            return firstElement;

        }
        public static int LastElement(int[] arr)
        {
            int lastElement = arr[arr.Length - 1];
            return lastElement;
        }
    }
}
