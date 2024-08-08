using Airports.Shared.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Airports.DataAccess.Interfaces;

public interface ITRepository<T> : INotify
    where T : class
{
    Task<List<T>> GetAsync(CancellationToken token = default);

    Task<T> GetAsync(int id, CancellationToken token = default);

    Task<T> CreateAsync(T entity, CancellationToken token = default);

    Task<T> UpdateAsync(T entity, CancellationToken token = default);

    Task<bool> RemoveAsync(int id, CancellationToken token = default);
}