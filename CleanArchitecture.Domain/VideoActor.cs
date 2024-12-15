using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class VideoActor : BaseDomainModel 
    {
        public int VideoId { get; set; }
        public Video? Video { get; set; }
        public int ActorId { get; set; }
        public Actor? Actor { get; set; }

    }
}
