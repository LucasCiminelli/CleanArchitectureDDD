using CleanArchitecture.Application.Features.Actors.Queries.GetActorsList;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{
    public  class VideosVm
    {
        public string? Nombre { get; set; }

        public int StreamerId { get; set; }

        public ICollection<ActorsVm>? Actores { get; set; }

    }
}
