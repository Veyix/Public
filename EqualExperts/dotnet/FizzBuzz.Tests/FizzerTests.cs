using Shouldly;
using Xunit;

namespace FizzBuzz.Tests
{
    public class FizzerTests
    {
        [Theory]
        [InlineData(
            "1 2 lucky 4 buzz fizz 7 8 fizz buzz",
            "fizz: 2 buzz: 2 fizzbuzz: 0 lucky: 1 integer: 5",
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10)]
        [InlineData(
            "fizzbuzz lucky fizzbuzz",
            "fizz: 0 buzz: 0 fizzbuzz: 2 lucky: 1 integer: 0",
            15, 30, 45)]
        [InlineData(
            "lucky fizz fizz fizz fizz",
            "fizz: 4 buzz: 0 fizzbuzz: 0 lucky: 1 integer: 0",
            3, 6, 9, 12, 18)]
        [InlineData(
            "buzz buzz buzz buzz lucky",
            "fizz: 0 buzz: 4 fizzbuzz: 0 lucky: 1 integer: 0",
            5, 10, 20, 25, 35)]
        public void GivenASetOfNumbers_WhenCallingTheFizzer_ThenTheExpectedOutputShouldBeProduced(
            string expectedOutput, string expectedReport, params int[] numbers)
        {
            var outputter = new TestOutputter();
            var tracker = new Tracker();
            var fizzer = new Fizzer(outputter, tracker);

            fizzer.Run(numbers);

            string actualOutput = outputter.GetOutput();
            actualOutput.ShouldBe(expectedOutput);

            string actualReport = tracker.GetTrackerReport();
            actualReport.ShouldBe(expectedReport);
        }
    }
}