
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class Director : BaseDomainModel
    {
        public Director()
        {
        }

        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public int VideoId { get; set; }

        public virtual Video? Video { get; set; }

    }
}
