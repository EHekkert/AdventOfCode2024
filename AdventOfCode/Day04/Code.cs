using System.Text.RegularExpressions;

namespace AdventOfCode.Day04
{
    public class Code
    {
        private int _maxX = 0;
        private int _maxY = 0;
        private List<KeyValuePair<int, int>> _xGrid = new();
        private List<KeyValuePair<int, int>> _mGrid = new();
        private List<KeyValuePair<int, int>> _aGrid = new();
        private List<KeyValuePair<int, int>> _sGrid = new();

        public int Part1(string[] lines)
        {
            var wordCount = 0;

            string xPattern = "[xX]";
            string mPattern = "[mM]";
            string aPattern = "[aA]";
            string sPattern = "[sS]";

            for (int i = 0; i < lines.Count(); i++)
            {
                _xGrid.AddRange(GetCoordinates(i, xPattern, lines[i]));
                _mGrid.AddRange(GetCoordinates(i, mPattern, lines[i]));
                _aGrid.AddRange(GetCoordinates(i, aPattern, lines[i]));
                _sGrid.AddRange(GetCoordinates(i, sPattern, lines[i]));
            }

            _maxX = lines[0].Length - 1;
            _maxY = lines.Count() - 1;

            foreach (var startCoordinate in _xGrid)
            {
                wordCount += FindXmas(startCoordinate);
            }

            return wordCount;
        }

        public int Part2(string[] lines)
        {
            var wordCount = 0;

            string mPattern = "[mM]";
            string aPattern = "[aA]";
            string sPattern = "[sS]";

            for (int i = 0; i < lines.Count(); i++)
            {
                _mGrid.AddRange(GetCoordinates(i, mPattern, lines[i]));
                _aGrid.AddRange(GetCoordinates(i, aPattern, lines[i]));
                _sGrid.AddRange(GetCoordinates(i, sPattern, lines[i]));
            }

            _maxX = lines[0].Length - 1;
            _maxY = lines.Count() - 1;

            foreach (var startCoordinate in _aGrid)
            {
                wordCount += FindXMas(startCoordinate) ? 1 : 0;
            }

            return wordCount;
        }

        private List<KeyValuePair<int, int>> GetCoordinates(int lineIndex, string pattern, string line)
        {
            var coordinates = new List<KeyValuePair<int, int>>();

            var positions = Regex.Matches(line, pattern).Select(x => x.Index).ToList();
            foreach (var position in positions)
            {
                coordinates.Add(new KeyValuePair<int, int>(lineIndex, position));
            }

            return coordinates;
        }

        private int FindXmas(KeyValuePair<int, int> startPosition)
        {
            var timesFound = 0;

            timesFound += FindUp(startPosition) ? 1 : 0;
            timesFound += FindUpAndForward(startPosition) ? 1 : 0;
            timesFound += FindForward(startPosition) ? 1 : 0;
            timesFound += FindDownAndForward(startPosition) ? 1 : 0;
            timesFound += FindDown(startPosition) ? 1 : 0;
            timesFound += FindDownAndBackward(startPosition) ? 1 : 0;
            timesFound += FindBackward(startPosition) ? 1 : 0;
            timesFound += FindUpAndBackward(startPosition) ? 1 : 0;

            return timesFound;
        }

        private bool FindXMas(KeyValuePair<int, int> middlePosition)
        {
            return ((FindUpAndBackwardToDownAndForward(middlePosition) || FindDownAndForwardToUpAndBackward(middlePosition)) && (FindUpAndForwardToDownAndBackward(middlePosition) || FindDownAndBackwardToUpAndForward(middlePosition)));
        }

        private bool FindForward(KeyValuePair<int, int> startPosition)
        {
            if (startPosition.Value > _maxX - 3)
            {
                //not enough space
                return false;
            }
            
            if (!_mGrid.Any(c => c.Key == startPosition.Key && c.Value == startPosition.Value + 1))
            {
                //No M found
                return false;
            }

            if (!_aGrid.Any(c => c.Key == startPosition.Key && c.Value == startPosition.Value + 2))
            {
                //No A found
                return false;
            }

            if (!_sGrid.Any(c => c.Key == startPosition.Key && c.Value == startPosition.Value + 3))
            {
                //No S found
                return false;
            }

            return true;
        }

        private bool FindBackward(KeyValuePair<int, int> startPosition)
        {
            if (startPosition.Value < 3)
            {
                //not enough space
                return false;
            }

            if (!_mGrid.Any(c => c.Key == startPosition.Key && c.Value == startPosition.Value - 1))
            {
                //No M found
                return false;
            }

            if (!_aGrid.Any(c => c.Key == startPosition.Key && c.Value == startPosition.Value - 2))
            {
                //No A found
                return false;
            }

            if (!_sGrid.Any(c => c.Key == startPosition.Key && c.Value == startPosition.Value - 3))
            {
                //No S found
                return false;
            }

            return true;
        }

        private bool FindDown(KeyValuePair<int, int> startPosition)
        {
            if (startPosition.Key > _maxY - 3)
            {
                //not enough space
                return false;
            }

            if (!_mGrid.Any(c => c.Key == startPosition.Key + 1 && c.Value == startPosition.Value))
            {
                //No M found
                return false;
            }

            if (!_aGrid.Any(c => c.Key == startPosition.Key + 2 && c.Value == startPosition.Value))
            {
                //No A found
                return false;
            }

            if (!_sGrid.Any(c => c.Key == startPosition.Key + 3 && c.Value == startPosition.Value))
            {
                //No S found
                return false;
            }

            return true;
        }

        private bool FindUp(KeyValuePair<int, int> startPosition)
        {
            if (startPosition.Key < 3)
            {
                //not enough space
                return false;
            }

            if (!_mGrid.Any(c => c.Key == startPosition.Key - 1 && c.Value == startPosition.Value))
            {
                //No M found
                return false;
            }

            if (!_aGrid.Any(c => c.Key == startPosition.Key - 2 && c.Value == startPosition.Value))
            {
                //No A found
                return false;
            }

            if (!_sGrid.Any(c => c.Key == startPosition.Key - 3 && c.Value == startPosition.Value))
            {
                //No S found
                return false;
            }

            return true;
        }

        private bool FindUpAndForward(KeyValuePair<int, int> startPosition)
        {
            if (startPosition.Key < 3 || startPosition.Value > _maxX - 3)
            {
                //not enough space
                return false;
            }

            if (!_mGrid.Any(c => c.Key == startPosition.Key - 1 && c.Value == startPosition.Value + 1))
            {
                //No M found
                return false;
            }

            if (!_aGrid.Any(c => c.Key == startPosition.Key - 2 && c.Value == startPosition.Value + 2))
            {
                //No A found
                return false;
            }

            if (!_sGrid.Any(c => c.Key == startPosition.Key - 3 && c.Value == startPosition.Value + 3))
            {
                //No S found
                return false;
            }

            return true;
        }

        private bool FindDownAndForward(KeyValuePair<int, int> startPosition)
        {
            if (startPosition.Key > _maxY - 3 || startPosition.Value > _maxX - 3)
            {
                //not enough space
                return false;
            }

            if (!_mGrid.Any(c => c.Key == startPosition.Key + 1 && c.Value == startPosition.Value + 1))
            {
                //No M found
                return false;
            }

            if (!_aGrid.Any(c => c.Key == startPosition.Key + 2 && c.Value == startPosition.Value + 2))
            {
                //No A found
                return false;
            }

            if (!_sGrid.Any(c => c.Key == startPosition.Key + 3 && c.Value == startPosition.Value + 3))
            {
                //No S found
                return false;
            }

            return true;
        }

        private bool FindUpAndBackward(KeyValuePair<int, int> startPosition)
        {
            if (startPosition.Key < 3 || startPosition.Value < 3)
            {
                //not enough space
                return false;
            }

            if (!_mGrid.Any(c => c.Key == startPosition.Key - 1 && c.Value == startPosition.Value - 1))
            {
                //No M found
                return false;
            }

            if (!_aGrid.Any(c => c.Key == startPosition.Key - 2 && c.Value == startPosition.Value - 2))
            {
                //No A found
                return false;
            }

            if (!_sGrid.Any(c => c.Key == startPosition.Key - 3 && c.Value == startPosition.Value - 3))
            {
                //No S found
                return false;
            }

            return true;
        }

        private bool FindDownAndBackward(KeyValuePair<int, int> startPosition)
        {
            if (startPosition.Key > _maxY - 3 || startPosition.Value < 3)
            {
                //not enough space
                return false;
            }

            if (!_mGrid.Any(c => c.Key == startPosition.Key + 1 && c.Value == startPosition.Value - 1))
            {
                //No M found
                return false;
            }

            if (!_aGrid.Any(c => c.Key == startPosition.Key + 2 && c.Value == startPosition.Value - 2))
            {
                //No A found
                return false;
            }

            if (!_sGrid.Any(c => c.Key == startPosition.Key + 3 && c.Value == startPosition.Value - 3))
            {
                //No S found
                return false;
            }

            return true;
        }

        private bool FindUpAndBackwardToDownAndForward(KeyValuePair<int, int> middlePosition)
        {
            if (middlePosition.Key < 1 || middlePosition.Key > _maxY - 1 || middlePosition.Value < 1 || middlePosition.Value > _maxX - 1)
            {
                //not enough space
                return false;
            }

            if (!_mGrid.Any(c => c.Key == middlePosition.Key - 1 && c.Value == middlePosition.Value - 1))
            {
                //No M found
                return false;
            }

            if (!_sGrid.Any(c => c.Key == middlePosition.Key + 1 && c.Value == middlePosition.Value + 1))
            {
                //No S found
                return false;
            }

            return true;
        }

        private bool FindUpAndForwardToDownAndBackward(KeyValuePair<int, int> middlePosition)
        {
            if (middlePosition.Key < 1 || middlePosition.Key > _maxY - 1 || middlePosition.Value < 1 || middlePosition.Value > _maxX - 1)
            {
                //not enough space
                return false;
            }

            if (!_mGrid.Any(c => c.Key == middlePosition.Key - 1 && c.Value == middlePosition.Value + 1))
            {
                //No M found
                return false;
            }

            if (!_sGrid.Any(c => c.Key == middlePosition.Key + 1 && c.Value == middlePosition.Value - 1))
            {
                //No S found
                return false;
            }

            return true;
        }

        private bool FindDownAndForwardToUpAndBackward(KeyValuePair<int, int> middlePosition)
        {
            if (middlePosition.Key < 1 || middlePosition.Key > _maxY - 1 || middlePosition.Value < 1 || middlePosition.Value > _maxX - 1)
            {
                //not enough space
                return false;
            }

            if (!_mGrid.Any(c => c.Key == middlePosition.Key + 1 && c.Value == middlePosition.Value + 1))
            {
                //No M found
                return false;
            }

            if (!_sGrid.Any(c => c.Key == middlePosition.Key - 1 && c.Value == middlePosition.Value - 1))
            {
                //No S found
                return false;
            }

            return true;
        }

        private bool FindDownAndBackwardToUpAndForward(KeyValuePair<int, int> middlePosition)
        {
            if (middlePosition.Key < 1 || middlePosition.Key > _maxY - 1 || middlePosition.Value < 1 || middlePosition.Value > _maxX - 1)
            {
                //not enough space
                return false;
            }

            if (!_mGrid.Any(c => c.Key == middlePosition.Key + 1 && c.Value == middlePosition.Value - 1))
            {
                //No M found
                return false;
            }

            if (!_sGrid.Any(c => c.Key == middlePosition.Key - 1 && c.Value == middlePosition.Value + 1))
            {
                //No S found
                return false;
            }

            return true;
        }
    }
}
