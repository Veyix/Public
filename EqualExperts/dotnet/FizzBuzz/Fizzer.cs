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
            
        }
    }
}