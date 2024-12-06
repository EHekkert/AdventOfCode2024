using AdventOfCode.Day06;

namespace AdventOfCodeTest
{
    public class Day06
    {
        private Code code = new Code();

        [Fact]
        public void Part1Example()
        {
            var lines = File.ReadAllLines(@".\Day06\ExampleData.aoc");
            var result = code.Part1(lines);

            Assert.Equal(41, result);
        }

        [Fact]
        public void Part1()
        {
            var lines = File.ReadAllLines(@".\Day06\Data.aoc");
            var result = code.Part1(lines);

            Assert.Equal(5531, result);
        }

        //[Fact]
        //public void Part2Example()
        //{
        //    var lines = File.ReadAllLines(@".\Day06\ExampleData.aoc");
        //    var result = code.Part2(lines);

        //    Assert.Equal(0, result);
        //}

        //[Fact]
        //public void Part2()
        //{
        //    var lines = File.ReadAllLines(@".\Day06\Data.aoc");
        //    var result = code.Part2(lines);

        //    Assert.Equal(0, result);
        //}
    }
}
