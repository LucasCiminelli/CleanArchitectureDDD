using CleanArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IActorRepository : IAsyncRepository<Actor>
    {

        Task<Actor> GetActorByNombre(string nombre);

        Task<IEnumerable<Actor>> GetActorsByUsername(string username);


    }
}
