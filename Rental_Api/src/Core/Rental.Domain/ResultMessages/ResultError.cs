namespace Rental.Domain.ResultMessages
{
    public class ResultError
    {
        public string Message { get; }
        public ResultError(string message)
        {
            Message = message;
        }
        public static implicit operator ResultError(string error) => new ResultError(error);
    }
}