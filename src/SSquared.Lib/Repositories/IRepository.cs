namespace SSquared.Lib.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken = default);

        Task<T?> GetAsync(int id, CancellationToken cancellationToken = default);
    }
}
