
using CleanArchitecture.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain
{
    public class Director : BaseDomainModel
    {
        public Director()
        {
        }

        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        [NotMapped]
        public string NombreCompleto => $"{Nombre} {Apellido}";

        public virtual ICollection<Video>? Videos { get; set; }

    }
}
