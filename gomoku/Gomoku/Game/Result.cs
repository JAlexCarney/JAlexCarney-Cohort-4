namespace Gomoku.Game
{
    public class Result
    {
        public bool IsSuccess { get; private set; }

        public string Message { get; private set; }

        public Result(string message)
            : this(message, false)
        {
        }

        public Result(string message, bool success)
        {
            Message = message;
            IsSuccess = success;
        }

        public override string ToString()
        {
            return $"Result{{success={IsSuccess}, message='{Message}\'}}";
        }
    }
}