namespace AdventOfCode.Day06
{
    [Serializable]
    public class LoopException : Exception
    {
        public LoopException()
        {
        }

        public LoopException(string message)
            : base(message)
        {
        }

        public LoopException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
