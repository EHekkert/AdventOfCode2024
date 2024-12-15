using AdventOfCode.Day15;

namespace AdventOfCodeTest
{
    public class Day15
    {
        private string dayNumber = "15";

        private Code code = new Code();
        
        [Fact]
        public void Part1Example1()
        {
            var line = File.ReadAllText(@$".\Day{dayNumber}\ExampleData1.aoc");
            var result = code.Part1(line);

            Assert.Equal(2028, result);
        }

        [Fact]
        public void Part1Example2()
        {
            var line = File.ReadAllText(@$".\Day{dayNumber}\ExampleData2.aoc");
            var result = code.Part1(line);

            Assert.Equal(10092, result);
        }

        [Fact]
        public void Part1()
        {
            var line = File.ReadAllText(@$".\Day{dayNumber}\Data.aoc");
            var result = code.Part1(line);

            Assert.Equal(1465523, result);
        }

        [Fact]
        public void Part21()
        {
            var line = File.ReadAllText(@$".\Day{dayNumber}\Data.aoc");
            var result = code.Part2(line);

            Assert.Equal(0, result);
        }
    }
}
