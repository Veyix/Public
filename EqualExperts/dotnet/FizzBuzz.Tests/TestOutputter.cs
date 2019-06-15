using System.Text;

namespace FizzBuzz.Tests
{
    public class TestOutputter : IOutputter
    {
        private readonly StringBuilder _outputBuilder = new StringBuilder();

        public void Output(string value) => _outputBuilder.Append(value);

        public string GetOutput() => _outputBuilder.ToString();
    }
}