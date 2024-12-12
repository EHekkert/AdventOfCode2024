using AdventOfCode.Day11;

namespace AdventOfCodeTest
{
    public class Day11
    {
        private string dayNumber = "11";

        private Code code = new Code();

        //[Theory]
        //[InlineData("125 17", "253000 1 7")]
        //[InlineData("253000 1 7", "253 0 2024 14168")]
        //[InlineData("253 0 2024 14168", "512072 1 20 24 28676032")]
        //public void TestStonesAfterBlink(string line, string expected)
        //{
        //    var data = line.Split(' ').Select(x => long.Parse(x)).ToArray();
        //    var expectedData = expected.Split(' ').Select(x => long.Parse(x)).ToArray();

        //    var result = code.StonesAfterBlink(data);

        //    Assert.Equal(expectedData, result);
        //}

        [Fact]
        public void Part1Example()
        {;
            var line = File.ReadAllText(@$".\Day{dayNumber}\ExampleData.aoc");
            var result = code.Part1(line);

            Assert.Equal(55312, result);
        }

        [Fact]
        public void Part1()
        {
            var line = File.ReadAllText(@$".\Day{dayNumber}\Data.aoc");
            var result = code.Part1(line);

            Assert.Equal(218956, result);
        }

        [Fact]
        public void Part2()
        {
            var line = File.ReadAllText(@$".\Day{dayNumber}\Data.aoc");
            var result = code.Part2(line);

            Assert.Equal(259593838049805, result);
        }
    }
}
