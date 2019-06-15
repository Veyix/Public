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

                if (i < numbers.Length - 1)
                {
                    _outputter.Output(" ");
                }
            }
        }

        private static bool IsMultipleOfThree(int number) => number % 3 == 0;
        private static bool IsMultipleOfFive(int number) => number % 5 == 0;
    }
}