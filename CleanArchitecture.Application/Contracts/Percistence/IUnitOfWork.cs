using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Contracts.Percistence
{
    public interface IUnitOfWork : IDisposable
    {
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;
        Task<int> Complete();
    }
}
