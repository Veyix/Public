using System;

namespace FizzBuzz
{
    public class ConsoleOutputter : IOutputter
    {
        public void Output(string value) => Console.Write(value);
    }
}