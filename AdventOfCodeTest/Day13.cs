using AdventOfCode.Day13;

namespace AdventOfCodeTest
{
    public class Day13
    {
        private string dayNumber = "13";

        private Code code = new Code();

        [Theory]
        [InlineData(35, 15, 5)]
        [InlineData(15, 36, 3)]
        [InlineData(37, 15, 1)]
        public void TestGetGCD(long a, long b, long expected)
        {
            var result = code.GCD(a, b);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(8400,5400,94,34,22,67,280)]
        [InlineData(12748, 12176, 26, 66, 67, 21, null)]
        [InlineData(7870, 6450, 17, 86, 84, 37, 200)]
        [InlineData(18641, 10279, 69, 23, 27, 71, null)]
        public void TestHowManyTokens(int x, int y, int buttonAX, int buttonAY, int buttonBX, int buttonBY, int? expected)
        {
            long? result = code.HowManyTokens(x, y, buttonAX, buttonAY, buttonBX, buttonBY);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1Example()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\ExampleData.aoc");
            var result = code.Part1(lines);

            Assert.Equal(480, result);
        }

        [Fact]
        public void Part1()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\Data.aoc");
            var result = code.Part1(lines);

            Assert.Equal(32067, result);
        }

        [Fact]
        public void Part2Example()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\ExampleData.aoc");
            var result = code.Part2(lines);

            Assert.Equal(480, result);
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
