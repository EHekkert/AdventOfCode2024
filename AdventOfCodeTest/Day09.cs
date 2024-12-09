using AdventOfCode.Day09;

namespace AdventOfCodeTest
{
    public class Day09
    {
        private string dayNumber = "09";

        private Code code = new Code();

        [Theory]
        [InlineData("12345", "0..111....22222")]
        [InlineData("2333133121414131402", "00...111...2...333.44.5555.6666.777.888899")]
        public void TestDiskMapToBlocks(string data, string expectedAsString)
        {
            var result = code.DiskMapToBlocks(data);

            var expected = expectedAsString.Select(x => x.ToString()).ToArray();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("0..111....22222", "022111222......")]
        [InlineData("00...111...2...333.44.5555.6666.777.888899", "0099811188827773336446555566..............")]
        public void TestFragment(string dataAsString, string expectedAsString)
        {
            var data = dataAsString.Select(x => x.ToString()).ToArray();
            var result = code.Fragment(data);

            var expected = expectedAsString.Select(x => x.ToString()).ToArray();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestCalculateChecksum()
        {
            var dataAsString = "0099811188827773336446555566..............";
            var data = dataAsString.Select(x => x.ToString()).ToArray();
            var result = code.CalculateChecksum(data);

            Assert.Equal(1928, result);
        }

        [Fact]
        public void Part1Example()
        {
            var line = File.ReadAllText(@$".\Day{dayNumber}\ExampleData.aoc");
            var result = code.Part1(line);

            Assert.Equal(1928, result);
        }

        [Fact]
        public void Part1()
        {
            var line = File.ReadAllText(@$".\Day{dayNumber}\Data.aoc");
            var result = code.Part1(line);

            Assert.Equal(6288599492129, result);
        }

        [Theory]
        [InlineData("0..111....22222", "0..111....22222")]
        [InlineData("00...111...2...333.44.5555.6666.777.888899", "00992111777.44.333....5555.6666.....8888..")]
        public void TestDeFragment(string dataAsString, string expectedAsString)
        {
            var data = dataAsString.Select(x => x.ToString()).ToArray();
            var result = code.Defragment(data);

            var expected = expectedAsString.Select(x => x.ToString()).ToArray();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2Example()
        {
            var line = File.ReadAllText(@$".\Day{dayNumber}\ExampleData.aoc");
            var result = code.Part2(line);

            Assert.Equal(2858, result);
        }

        [Fact]
        public void Part2()
        {
            var line = File.ReadAllText(@$".\Day{dayNumber}\Data.aoc");
            var result = code.Part2(line);

            Assert.Equal(6321896265143, result);
        }
    }
}
