using CleanArchitecture.Application.Features.Actors.Queries.Vms;

namespace CleanArchitecture.Application.Features.Videos.Queries.Vms
{
    public class VideosVm
    {
        public string? Nombre { get; set; }

        public int StreamerId { get; set; }

        public ICollection<ActorsVm>? Actores { get; set; }

    }
}
