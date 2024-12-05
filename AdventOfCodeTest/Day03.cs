namespace AdventOfCodeTest
{
    public class Day03
    {
        [Fact]
        public void Part1Example()
        {
            var line = File.ReadAllText(@".\Day03\ExampleData1.aoc");
            var result = AdventOfCode.Day03.Code.Part1(line);

            Assert.Equal(161, result);
        }

        [Fact]
        public void Part1()
        {
            var line = File.ReadAllText(@".\Day03\Data.aoc");
            var result = AdventOfCode.Day03.Code.Part1(line);

            Assert.Equal(161289189, result);
        }

        [Fact]
        public void Part2Example()
        {
            var line = File.ReadAllText(@".\Day03\ExampleData2.aoc");
            var result = AdventOfCode.Day03.Code.Part2(line);

            Assert.Equal(48, result);
        }

        [Fact]
        public void Part2()
        {
            var line = File.ReadAllText(@".\Day03\Data.aoc");
            var result = AdventOfCode.Day03.Code.Part2(line);

            Assert.Equal(83595109, result);
        }
    }
}
