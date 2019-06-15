using System;

namespace FizzBuzz
{
    public class Tracker
    {
        private int _numberCount = 0;
        private int _fizzCount = 0;
        private int _buzzCount = 0;
        private int _fizzBuzzCount = 0;
        private int _luckyCount = 0;

        public string GetTrackerReport() =>
            $"fizz: {_fizzCount} buzz: {_buzzCount} fizzbuzz: {_fizzBuzzCount} lucky: {_luckyCount} integer: {_numberCount}";

        public void TrackNumber() => _numberCount++;
        public void TrackFizz() => _fizzCount++;
        public void TrackBuzz() => _buzzCount++;
        public void TrackFizzBuzz() => _fizzBuzzCount++;
        public void TrackLucky() => _luckyCount++;
    }
}