using AdventOfCode.Day08;

namespace AdventOfCodeTest
{
    public class Day08
    {
        private string dayNumber = "08";

        private Code code = new Code();

        [Fact]
        public void Part1Example()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\ExampleData.aoc");
            var result = code.Part1(lines);

            Assert.Equal(14, result);
        }

        [Fact]
        public void Part1()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\Data.aoc");
            var result = code.Part1(lines);

            Assert.Equal(359, result);
        }

        [Fact]
        public void Part2Example()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\ExampleData.aoc");
            var result = code.Part2(lines);

            Assert.Equal(34, result);
        }

        [Fact]
        public void Part2()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\Data.aoc");
            var result = code.Part2(lines);

            Assert.Equal(1293, result);
        }
    }
}
