namespace AdventOfCodeTest
{
    public class Day02
    {
        [Fact]
        public void Part1Example()
        {
            var lines = File.ReadAllLines(@".\Day02\ExampleData.aoc");
            var result = AdventOfCode.Day02.Code.Part1(lines);

            Assert.Equal(2, result);
        }

        [Fact]
        public void Part1()
        {
            var lines = File.ReadAllLines(@".\Day02\Data.aoc");
            var result = AdventOfCode.Day02.Code.Part1(lines);

            Assert.Equal(224, result);
        }

        [Fact]
        public void Part2Example()
        {
            var lines = File.ReadAllLines(@".\Day02\ExampleData.aoc");
            var result = AdventOfCode.Day02.Code.Part2(lines);

            Assert.Equal(4, result);
        }

        [Fact]
        public void Part2()
        {
            var lines = File.ReadAllLines(@".\Day02\Data.aoc");
            var result = AdventOfCode.Day02.Code.Part2(lines);

            Assert.Equal(293, result);
        }
    }
}
