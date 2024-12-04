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
    internal class DirectorRepository : RepositoryBase<Director>, IDirectorRepository
    {
        public DirectorRepository(StreamerDbContext context) : base(context)
        {
        }

        public async Task<Director> GetDirectorByName(string directorName)
        {
            return await _context!.Directores!.Where(d => d.Nombre == directorName).FirstOrDefaultAsync();
        }
    }
}
