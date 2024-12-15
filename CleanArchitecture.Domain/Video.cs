using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class Video : BaseDomainModel
    {
        public Video()
        {
            Actores = new HashSet<Actor>();
        }

        public string? Nombre { get; set; }

        public int StreamerId { get; set; }

        public virtual Streamer? Streamer { get; set; }

        public int DirectorId { get; set; }

        public virtual Director? Director { get; set; }

        public virtual ICollection<Actor>? Actores { get; set; }

        public virtual ICollection<VideoActor>? VideoActors { get; set; }
    }
}
