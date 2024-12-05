namespace AdventOfCodeTest
{
    public class Day01
    {
        [Fact]
        public void Part1Example()
        {
            var lines = File.ReadAllLines(@".\Day01\ExampleData.aoc");
            var result = AdventOfCode.Day01.Code.Part1(lines);

            Assert.Equal(11, result);
        }

        [Fact]
        public void Part1()
        {
            var lines = File.ReadAllLines(@".\Day01\Data.aoc");
            var result = AdventOfCode.Day01.Code.Part1(lines);

            Assert.Equal(1222801, result);
        }

        [Fact]
        public void Part2Example()
        {
            var lines = File.ReadAllLines(@".\Day01\ExampleData.aoc");
            var result = AdventOfCode.Day01.Code.Part2(lines);

            Assert.Equal(31, result);
        }

        [Fact]
        public void Part2()
        {
            var lines = File.ReadAllLines(@".\Day01\Data.aoc");
            var result = AdventOfCode.Day01.Code.Part2(lines);

            Assert.Equal(22545250, result);
        }
    }
}