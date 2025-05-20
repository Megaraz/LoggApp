using AppLogic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
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
                throw new ArgumentException($"Something went wrong, {e.Message}");

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
                throw new ArgumentException($"Something went wrong, {e.Message}");

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
                throw new ArgumentException($"Something went wrong, {e.Message}");

            }

        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {

            try
            {
                var dbSet = _dbContext.Set<T>();

                var entity = await dbSet.FindAsync(id);
                if (entity == null) return false;

                dbSet.Remove(entity!);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                throw new ArgumentException($"Something went wrong, {e.Message}");

            }
        }

    }
}
