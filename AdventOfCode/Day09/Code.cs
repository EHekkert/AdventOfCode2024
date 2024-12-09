using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day09
{
    public class Code
    {
        public long Part1(string line)
        {
            var blocks = DiskMapToBlocks(line);
            var fragmented = Fragment(blocks);
            return CalculateChecksum(fragmented);
        }

        public long Part2(string line)
        {
            var blocks = DiskMapToBlocks(line);
            var defragmented = Defragment(blocks);
            return CalculateChecksum(defragmented);
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

        public string[] Fragment(string[] line)
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

        public string[] Defragment(string[] line)
        {
            string freeSpaceMap = GetFreeSapceMap(line);

            int index = line.Length - 1;

            do
            {
                if (line[index] != ".")
                {
                    var startIndex = Array.IndexOf(line, line[index]);
                    var length = index - startIndex + 1;

                    var pattern = @$"\.{{{length}}}";
                    Match match = Regex.Match(freeSpaceMap, pattern);
                    if (match.Success)
                    {
                        var freeSpaceIndex = match.Index;

                        if (freeSpaceIndex < startIndex)
                        {
                            for (int i = 0; i < length; i++)
                            {
                                line[freeSpaceIndex + i] = line[startIndex + i];
                                line[startIndex + i] = ".";
                            }

                            //Before swapped free space
                            int segment1Index = 0;
                            int segment1Length = freeSpaceIndex;

                            //File blocks that were swapped
                            int segment2Index = startIndex;
                            int segment2Length = length;

                            //Between swapped free space and file blocks
                            int segment3Index = freeSpaceIndex + length;
                            int segment3Length = startIndex - freeSpaceIndex - length;

                            //Free space that was swapped
                            int segment4Index = freeSpaceIndex;
                            int segment4Length = length;

                            //After swapped file blocks
                            int segment5Index = startIndex + length;
                            int segment5Length = freeSpaceMap.Length - startIndex - length;

                            StringBuilder sb = new();
                            sb.Append(freeSpaceMap.Substring(segment1Index, segment1Length));
                            sb.Append(freeSpaceMap.Substring(segment2Index, segment2Length));
                            sb.Append(freeSpaceMap.Substring(segment3Index, segment3Length));
                            sb.Append(freeSpaceMap.Substring(segment4Index, segment4Length));
                            sb.Append(freeSpaceMap.Substring(segment5Index, segment5Length));

                            freeSpaceMap = sb.ToString();
                        }
                    }

                    index = startIndex - 1;
                }
                else
                {
                    index--;
                }
            } while (index >= 0);

            return line;
        }

        public long CalculateChecksum(string[] line)
        {
            long checksum = 0;

            for (int multiplier = 0; multiplier < line.Length; multiplier++) {
                if (line[multiplier] != ".")
                {
                    checksum += Convert.ToInt16(line[multiplier]) * multiplier;
                }
            };

            return checksum;
        }

        private string GetFreeSapceMap(string[] line)
        {
            StringBuilder sb = new();
            for (int i = 0; i < line.Length; i++)
            {
                char character = line[i] == "." ? '.' : 'X';
                sb.Append(character);
            }

            return sb.ToString();
        }
    }
}
