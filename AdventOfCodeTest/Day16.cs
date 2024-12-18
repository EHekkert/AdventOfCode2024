using AdventOfCode.Day16;

namespace AdventOfCodeTest
{
    public class Day16
    {
        private string dayNumber = "16";

        private Code code = new Code();
        
        [Fact]
        public void Part1Example1()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\ExampleData1.aoc");
            var result = code.Part1(lines);

            Assert.Equal(7036, result);
        }

        [Fact]
        public void Part1Example2()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\ExampleData2.aoc");
            var result = code.Part1(lines);

            Assert.Equal(11048, result);
        }

        [Fact]
        public void Part1TestData()
        {
            var lines = File.ReadAllLines(@$".\Day{dayNumber}\TestData.txt");
            var result = code.Part1(lines);

            Assert.Equal(7038, result);
        }

        [Fact]
        public void Part1()
        {
            // Set the stack size you need (e.g., 10 MB)
            int stackSize = 20 * 1024 * 1024; // 10 MB in bytes

            // Create the thread, specifying the stack size
            Thread thread = new Thread(new ThreadStart(Test), stackSize);

            // Start the thread
            thread.Start();

            // Wait for the thread to finish if necessary
            thread.Join();

        }

        private void Test()
        {
            try
            {
                var lines = File.ReadAllLines(@$".\Day{dayNumber}\Data.aoc");
                var result = code.Part1(lines);

                Assert.Equal(102460, result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        //[Fact]
        //public void Part2Example1()
        //{
        //    var line = File.ReadAllText(@$".\Day{dayNumber}\ExampleData3.aoc");
        //    var result = code.Part2(line);

        //    Assert.Equal(618, result);
        //}

        //[Fact]
        //public void Part2Example2()
        //{
        //    var line = File.ReadAllText(@$".\Day{dayNumber}\ExampleData2.aoc");
        //    var result = code.Part2(line);

        //    Assert.Equal(9021, result);
        //}

        //[Fact]
        //public void Part2()
        //{
        //    var line = File.ReadAllText(@$".\Day{dayNumber}\Data.aoc");
        //    var result = code.Part2(line);

        //    Assert.Equal(1471049, result);
        //}
    }
}
