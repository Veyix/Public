using System;

namespace FizzBuzz
{
    public static class Program
    {
        private static readonly int[] Numbers =
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
            11, 12, 13, 14, 15, 16, 17, 18, 19, 20
        };
        
        public static void Main()
        {
            Console.WriteLine("=== Running Fizzer ===");

            var outputter = new ConsoleOutputter();
            var fizzer = new Fizzer(outputter);

            fizzer.Run(Numbers);

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
