namespace SSquared.Lib.Exceptions
{
    public class NotFoundException<T> : Exception
    {
        public NotFoundException(int id)
            : base($"Could not find {typeof(T).Name} by ID {id}")
        {
            Id = id;
        }

        public int Id { get; }
    }
}
