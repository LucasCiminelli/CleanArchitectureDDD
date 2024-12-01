using CleanArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IDirectorRepository : IAsyncRepository<Director>
    {

        Task<Director> GetDirectorByName(string directorName);

    }
}
