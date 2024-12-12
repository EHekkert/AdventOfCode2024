using AdventOfCode.Day12;

namespace AdventOfCodeTest
{
    public class Day12
    {
        private string dayNumber = "12";

        private Code code = new Code();
        
        [Fact]
        public void Part1Example()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\ExampleData.aoc");
            var result = code.Part1(lines);

            Assert.Equal(1930, result);
        }

        [Fact]
        public void Part1()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\Data.aoc");
            var result = code.Part1(lines);

            Assert.Equal(1486324, result);
        }

        [Fact]
        public void Part2Example()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\ExampleData.aoc");
            var result = code.Part2(lines);

            Assert.Equal(1206, result);
        }

        [Fact]
        public void Part2()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\Data.aoc");
            var result = code.Part2(lines);

            Assert.Equal(0, result);
        }
    }
}
