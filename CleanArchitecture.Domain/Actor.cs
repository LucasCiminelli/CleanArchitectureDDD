
using CleanArchitecture.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain
{
    public class Actor : BaseDomainModel
    {
        public Actor() { 
            Videos = new HashSet<Video>();
        }
        
        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        [NotMapped]
        public string NombreCompleto => $"{Nombre} {Apellido}";

        public virtual ICollection<Video>? Videos { get; set; }

        public virtual ICollection<VideoActor>? VideoActors { get; set; }

    }
}
