using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class StreamerRepository : RepositoryBase<Streamer>, IStreamerRepository
    {
        public StreamerRepository(StreamerDbContext context) : base(context)
        { }

        public async Task<Streamer> GetStreamerByNombre(string nombre)
        {
            return await _context.Streamers!.Where(s => s.Nombre == nombre).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Streamer>> GetStreamersByUsername(string username)
        {
            return await _context.Streamers!.Where(s => s.CreatedBy == username).ToListAsync();
        }
    }
}
