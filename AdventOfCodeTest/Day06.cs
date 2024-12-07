using AdventOfCode.Day06;

namespace AdventOfCodeTest
{
    public class Day06
    {
        private Code code = new Code();
        private AdventOfCode.Day06.WithHelp.Code codeWithHelp = new ();

        [Fact]
        public void Part1Example()
        {
            var lines = File.ReadAllLines(@".\Day06\ExampleData.aoc");
            var result = code.Part1(lines);

            Assert.Equal(41, result);
        }

        [Fact]
        public void Part1WithHelpExample()
        {
            var lines = File.ReadAllLines(@".\Day06\ExampleData.aoc");
            var result = codeWithHelp.Part1(lines);

            Assert.Equal(41, result);
        }

        [Fact]
        public void Part1()
        {
            var lines = File.ReadAllLines(@".\Day06\Data.aoc");
            var result = code.Part1(lines);

            Assert.Equal(5531, result);
        }

        [Fact]
        public void Part1WithHelp()
        {
            var lines = File.ReadAllLines(@".\Day06\Data.aoc");
            var result = codeWithHelp.Part1(lines);

            Assert.Equal(5531, result);
        }

        [Fact]
        public void Part2WithHelpExample()
        {
            var lines = File.ReadAllLines(@".\Day06\ExampleData.aoc");
            var result = codeWithHelp.Part2(lines);

            Assert.Equal(6, result);
        }

        [Fact]
        public void Part2Example()
        {
            var lines = File.ReadAllLines(@".\Day06\ExampleData.aoc");
            var result = code.Part2(lines);

            Assert.Equal(6, result);
        }

        [Fact(Skip = "Takes way too long")]
        public void Part2()
        {
            var lines = File.ReadAllLines(@".\Day06\Data.aoc");
            var result = code.Part2(lines);

            Assert.Equal(2165, result);
        }

        [Fact]
        public void Part2WithHelp()
        {
            var lines = File.ReadAllLines(@".\Day06\Data.aoc");
            var result = codeWithHelp.Part2(lines);

            Assert.Equal(2165, result);
        }
    }
}
