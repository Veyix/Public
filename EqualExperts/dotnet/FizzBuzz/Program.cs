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
            var tracker = new Tracker();
            var fizzer = new Fizzer(outputter, tracker);

            fizzer.Run(Numbers);

            string report = tracker.GetTrackerReport();
            outputter.Output($" {report}");

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
