using AdventOfCode.Day14;

namespace AdventOfCodeTest
{
    public class Day14
    {
        private string dayNumber = "14";

        private Code code = new Code();
        
        [Fact]
        public void Part1Example()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\ExampleData.aoc");
            var result = code.Part1(lines, 11, 7, 100);

            Assert.Equal(12, result);
        }

        [Fact]
        public void Part1()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\Data.aoc");
            var result = code.Part1(lines,101, 103, 100);

            Assert.Equal(220971520, result);
        }

        //[Fact]
        //public void Part2Example()
        //{
        //    var lines = File.ReadAllLines(@$".\Day{dayNumber}\ExampleData.aoc");
        //    var result = code.Part2(lines);

        //    Assert.Equal(480, result);
        //}

        //[Fact]
        //public void Part2()
        //{
        //    var lines = File.ReadAllLines(@$".\Day{dayNumber}\Data.aoc");
        //    var result = code.Part2(lines);

        //    Assert.Equal(0, result);
        //}
    }
}
