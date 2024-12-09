using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class ActorRepository : RepositoryBase<Actor>, IActorRepository
    {
        public ActorRepository(StreamerDbContext context) : base(context)
        {
        }

        public async Task<Actor> GetActorByNombre(string nombre)
        {
            return await _context.Actores!.Where(a => a.Nombre == nombre).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Actor>> GetActorsByUsername(string username)
        {
            return await _context.Actores!.Where(a => a.CreatedBy == username).ToListAsync();
        }
    }
}
