using AppLogic;
using AppLogic.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppLogic.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly LoggAppContext _dbContext;

        public GenericRepo(LoggAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Create failed for {typeof(T).Name}: {e.Message}", e);
            }
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            try
            {
                return await _dbContext.Set<T>().FindAsync(id);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"GetById failed for {typeof(T).Name}: {e.Message}", e);
            }
        }

        public async Task<List<T>?> GetAllAsync()
        {
            try
            {
                return await _dbContext.Set<T>().ToListAsync();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"GetAll failed for {typeof(T).Name}: {e.Message}", e);
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _dbContext.Set<T>().Update(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Update failed for {typeof(T).Name}: {e.Message}", e);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var dbSet = _dbContext.Set<T>();
                var entity = await dbSet.FindAsync(id);

                if (entity == null) return false;

                dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Delete failed for {typeof(T).Name}: {e.Message}", e);
            }
        }
    }
}
