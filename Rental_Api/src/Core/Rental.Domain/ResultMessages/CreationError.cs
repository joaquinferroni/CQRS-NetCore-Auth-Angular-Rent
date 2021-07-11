namespace Rental.Domain.ResultMessages
{
    public class CreationError : ResultError
    {
        public CreationError(string message) : base(message)
        {
        }
    }
}