using AdventOfCode.Day10;

namespace AdventOfCodeTest
{
    public class Day10
    {
        private string dayNumber = "10";

        private Code code = new Code();
        
        [Fact]
        public void Part1Example()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\ExampleData.aoc");
            var result = code.Part1(lines);

            Assert.Equal(36, result);
        }

        [Fact]
        public void Part1()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\Data.aoc");
            var result = code.Part1(lines);

            Assert.Equal(746, result);
        }

        [Fact]
        public void Part2Example()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\ExampleData.aoc");
            var result = code.Part2(lines);

            Assert.Equal(81, result);
        }

        [Fact]
        public void Part2()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\Data.aoc");
            var result = code.Part2(lines);

            Assert.Equal(1541, result);
        }
    }
}
