using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IStreamerRepository : IAsyncRepository<Streamer>
    {

        public Task<IEnumerable<Streamer>> GetStreamersByUsername(string username);

        public Task<Streamer> GetStreamerByNombre(string nombre);

    }
}
