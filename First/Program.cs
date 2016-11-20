using System;

namespace First
{
    class Program
    {
        static void Main(string[] args)
        {
            var elements = new SumPair(new int[] { 0, -1, 1, 3, 4, 5, 30, 90 }, 0);
            Console.WriteLine(elements);

        }
    }
}
