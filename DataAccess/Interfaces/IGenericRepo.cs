using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<List<T>?> GetAllAsync();
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);

    }
}
