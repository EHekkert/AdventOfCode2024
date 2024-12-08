using AdventOfCode.Day07;

namespace AdventOfCodeTest
{
    public class Day07
    {
        private string dayNumber = "07";

        private Code code = new Code();

        [Fact]
        public void Part1Example()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\ExampleData.aoc");
            var result = code.Part1(lines);

            Assert.Equal(3749, result);
        }

        [Fact]
        public void Part1()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\Data.aoc");
            var result = code.Part1(lines);

            Assert.Equal(2654749936343, result);
        }

        [Fact]
        public void Part2Example()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\ExampleData.aoc");
            var result = code.Part2(lines);

            Assert.Equal(11387, result);
        }

        [Fact]
        public void Part2()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\Data.aoc");
            var result = code.Part2(lines);

            Assert.Equal(124060392153684, result);
        }
    }
}
