namespace AppLogic.Repositories.Interfaces
{
    /// <summary>
    /// Generic repository interface for CRUD operations on entities of type T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepo<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<List<T>?> GetAllAsync();
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);

    }
}
