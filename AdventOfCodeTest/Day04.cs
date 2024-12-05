using AdventOfCode.Day04;

namespace AdventOfCodeTest
{
    public class Day04
    {
        private AdventOfCode.Day04.Code code = new Code();

        [Fact]
        public void Part1Example()
        {
            var lines = File.ReadAllLines(@".\Day04\ExampleData.aoc");
            var result = code.Part1(lines);

            Assert.Equal(18, result);
        }

        [Fact]
        public void Part1()
        {
            var lines = File.ReadAllLines(@".\Day04\Data.aoc");
            var result = code.Part1(lines);

            Assert.Equal(2567, result);
        }

        [Fact]
        public void Part2Example()
        {
            var lines = File.ReadAllLines(@".\Day04\ExampleData.aoc");
            var result = code.Part2(lines);

            Assert.Equal(9, result);
        }

        [Fact]
        public void Part2()
        {
            var lines = File.ReadAllLines(@".\Day04\Data.aoc");
            var result = code.Part2(lines);

            Assert.Equal(2029, result);
        }
    }
}
