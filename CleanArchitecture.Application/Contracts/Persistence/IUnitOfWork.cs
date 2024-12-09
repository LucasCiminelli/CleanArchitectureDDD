using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        
        IStreamerRepository StreamerRepository { get; }
        IVideoRepository VideoRepository { get; }
        IDirectorRepository DirectorRepository { get; }
        IActorRepository ActorRepository { get; }
        
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;

        Task<int> Complete();
    }
}
