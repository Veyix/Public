using Shouldly;
using Xunit;

namespace FizzBuzz.Tests
{
    public class FizzerTests
    {
        [Theory]
        [InlineData("1 2 lucky 4 buzz fizz 7 8 fizz buzz", 1, 2, 3, 4, 5, 6, 7, 8, 9, 10)]
        [InlineData("fizzbuzz lucky fizzbuzz", 15, 30, 45)]
        [InlineData("lucky fizz fizz fizz fizz", 3, 6, 9, 12, 18)]
        [InlineData("buzz buzz buzz buzz lucky", 5, 10, 20, 25, 35)]
        public void GivenASetOfNumbers_WhenCallingTheFizzer_ThenTheExpectedOutputShouldBeProduced(
            string expectedOutput, params int[] numbers)
        {
            var outputter = new TestOutputter();
            var fizzer = new Fizzer(outputter);

            fizzer.Run(numbers);

            string actualOutput = outputter.GetOutput();
            actualOutput.ShouldBe(expectedOutput);
        }
    }
}