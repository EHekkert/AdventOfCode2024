using AdventOfCode.Day05;

namespace AdventOfCodeTest
{
    public class Day05
    {
        private Code code = new Code();

        [Fact]
        public void Part1Example()
        {
            var lines = File.ReadAllLines(@".\Day05\ExampleData.aoc");
            var result = code.Part1(lines);

            Assert.Equal(143, result);
        }

        [Fact]
        public void Part1()
        {
            var lines = File.ReadAllLines(@".\Day05\Data.aoc");
            var result = code.Part1(lines);

            Assert.Equal(5064, result);
        }

        [Fact]
        public void Part2Example()
        {
            var lines = File.ReadAllLines(@".\Day05\ExampleData.aoc");
            var result = code.Part2(lines);

            Assert.Equal(123, result);
        }

        [Fact]
        public void Part2()
        {
            var lines = File.ReadAllLines(@".\Day05\Data.aoc");
            var result = code.Part2(lines);

            Assert.Equal(5152, result);
        }
    }
}
