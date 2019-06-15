using System;

namespace FizzBuzz
{
    public class Fizzer
    {
        private readonly IOutputter _outputter;

        public Fizzer(IOutputter outputter)
        {
            _outputter = outputter ?? throw new ArgumentNullException(nameof(outputter));
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
            }
            else
            {
                bool isMultipleOfThree = IsMultipleOfThree(number);
                bool isMultipleOfFive = IsMultipleOfFive(number);

                if (isMultipleOfThree && isMultipleOfFive)
                {
                    _outputter.Output("fizzbuzz");
                }
                else if (isMultipleOfThree)
                {
                    _outputter.Output("fizz");
                }
                else if (isMultipleOfFive)
                {
                    _outputter.Output("buzz");
                }
                else
                {
                    _outputter.Output(number.ToString());
                }
            }
        }

        private static bool DoesNumberContainThree(int number) => number.ToString().Contains("3");
        private static bool IsMultipleOfThree(int number) => number % 3 == 0;
        private static bool IsMultipleOfFive(int number) => number % 5 == 0;
    }
}