using System.Text;

namespace AdventOfCode.Day09
{
    public class Code
    {
        public long Part1(string line)
        {
            var blocks = DiskMapToBlocks(line);
            var defragmented = Defragment(blocks);
            return CalculateChecksum(defragmented);
        }

        public long Part2(string line)
        {
            throw new NotImplementedException();
        }

        public string[] DiskMapToBlocks(string line)
        {
            string[] result = Array.Empty<string>();

            int blockId = 0;

            StringBuilder blocks = new();
            for (int i = 0; i < line.Length; i++)
            {
                int currentSize = result.Length;
                int newSize = (int)char.GetNumericValue(line[i]) + result.Length;
                string[] tempArray =  new string[newSize];
                Array.Copy(result, tempArray, currentSize);

                var blockIdAsString = blockId.ToString();
                if (i % 2 == 0)
                {
                    for (int j = currentSize; j < newSize; j++)
                    {
                        tempArray[j] = blockIdAsString;
                    }
                    blockId++;
                }
                else
                {
                    for(int j = currentSize; j < newSize; j++)
                    {
                        tempArray[j] = ".";
                    }
                }

                result = tempArray;
            }

            return result;
        }

        public string[] Defragment(string[] line)
        {
            int index1 = 0;
            int index2 = line.Length - 1;

            do
            {
                if (line[index1] == ".")
                {
                    string replaceWith = "";
                    do
                    {
                        replaceWith = line[index2];
                        index2--;
                    } while (replaceWith == "." && index2 > index1);

                    if (replaceWith != ".")
                    {
                        line[index2+1] = line[index1];
                        line[index1] = replaceWith;
                    }
                }

                index1++;
            } while (index1 < index2);

            return line;
        }

        public long CalculateChecksum(string[] line)
        {
            long checksum = 0;
            int multiplier = 0;

            do
            {
                checksum += Convert.ToInt16(line[multiplier]) * multiplier;
                multiplier++;
            } while (line[multiplier] != ".");

            return checksum;
        }
    }
}
