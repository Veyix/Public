using Shouldly;
using Xunit;

namespace FizzBuzz.Tests
{
    public class TrackerTests
    {
        private readonly Tracker _tracker;

        public TrackerTests()
        {
            _tracker = new Tracker();
        }
        
        [Fact]
        public void GivenNothingIsTracked_WhenTrackNumberIsCalled_ThenTheExpectedReportShouldBeReturned()
        {
            _tracker.TrackNumber();

            string actualReport = _tracker.GetTrackerReport();
            actualReport.ShouldBe("fizz: 0 buzz: 0 fizzbuzz: 0 lucky: 0 integer: 1");
        }

        [Fact]
        public void GivenNothingIsTracked_WhenTrackFizzIsCalled_ThenTheExpectedReportShouldBeReturned()
        {
            _tracker.TrackFizz();

            string actualReport = _tracker.GetTrackerReport();
            actualReport.ShouldBe("fizz: 1 buzz: 0 fizzbuzz: 0 lucky: 0 integer: 0");
        }

        [Fact]
        public void GivenNothingIsTracked_WhenTrackBuzzIsCalled_ThenTheExpectedReportShouldBeReturned()
        {
            _tracker.TrackBuzz();

            string actualReport = _tracker.GetTrackerReport();
            actualReport.ShouldBe("fizz: 0 buzz: 1 fizzbuzz: 0 lucky: 0 integer: 0");
        }

        [Fact]
        public void GivenNothingIsTracked_WhenTrackFizzBuzzIsCalled_ThenTheExpectedReportShouldBeReturned()
        {
            _tracker.TrackFizzBuzz();

            string actualReport = _tracker.GetTrackerReport();
            actualReport.ShouldBe("fizz: 0 buzz: 0 fizzbuzz: 1 lucky: 0 integer: 0");
        }

        [Fact]
        public void GivenNothingIsTracked_WhenTrackLuckyIsCalled_ThenTheExpectedReportShouldBeReturned()
        {
            _tracker.TrackLucky();

            string actualReport = _tracker.GetTrackerReport();
            actualReport.ShouldBe("fizz: 0 buzz: 0 fizzbuzz: 0 lucky: 1 integer: 0");
        }

        [Fact]
        public void GivenNothingIsTracked_WhenASeriesOfCallsAreMadeToTheTrackingMethods_ThenTheExpectedReportShouldBeReturned()
        {
            _tracker.TrackLucky();
            _tracker.TrackNumber();
            _tracker.TrackFizzBuzz();
            _tracker.TrackNumber();
            _tracker.TrackBuzz();
            _tracker.TrackLucky();
            _tracker.TrackBuzz();
            _tracker.TrackNumber();
            _tracker.TrackLucky();
            _tracker.TrackFizz();
            _tracker.TrackFizz();
            _tracker.TrackFizzBuzz();
            _tracker.TrackFizzBuzz();
            _tracker.TrackLucky();

            string actualReport = _tracker.GetTrackerReport();
            actualReport.ShouldBe("fizz: 2 buzz: 2 fizzbuzz: 3 lucky: 4 integer: 3");
        }
    }
}