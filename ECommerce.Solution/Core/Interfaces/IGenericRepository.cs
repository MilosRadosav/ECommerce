using Core.Interfaces.Specifications;
using Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        Task<T> GetEntityWithSpec(ISpecification<T> specification);
        Task<List<T>> ListAsync(ISpecification<T> spec);

        Task<int> CountAsync(ISpecification<T> specification);
    }
}
