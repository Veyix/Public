using System;

namespace FizzBuzz
{
    public class Fizzer
    {
        private readonly IOutputter _outputter;
        private readonly Tracker _tracker;

        public Fizzer(IOutputter outputter, Tracker tracker)
        {
            _outputter = outputter ?? throw new ArgumentNullException(nameof(outputter));
            _tracker = tracker ?? throw new ArgumentNullException(nameof(tracker));
        }

        public void Run(params int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                int number = numbers[i];

                ProcessNumber(number);

                if (!IsLastNumber(i, numbers.Length))
                {
                    _outputter.Output(" ");
                }
            }
        }

        private static bool IsLastNumber(int currentIndex, int numberCount) =>
            currentIndex == numberCount - 1;

        private void ProcessNumber(int number)
        {
            if (DoesNumberContainThree(number))
            {
                _outputter.Output("lucky");
                _tracker.TrackLucky();
            }
            else
            {
                bool isMultipleOfThree = IsMultipleOfThree(number);
                bool isMultipleOfFive = IsMultipleOfFive(number);

                if (isMultipleOfThree && isMultipleOfFive)
                {
                    _outputter.Output("fizzbuzz");
                    _tracker.TrackFizzBuzz();
                }
                else if (isMultipleOfThree)
                {
                    _outputter.Output("fizz");
                    _tracker.TrackFizz();
                }
                else if (isMultipleOfFive)
                {
                    _outputter.Output("buzz");
                    _tracker.TrackBuzz();
                }
                else
                {
                    _outputter.Output(number.ToString());
                    _tracker.TrackNumber();
                }
            }
        }

        private static bool DoesNumberContainThree(int number) => number.ToString().Contains("3");
        private static bool IsMultipleOfThree(int number) => number % 3 == 0;
        private static bool IsMultipleOfFive(int number) => number % 5 == 0;
    }
}