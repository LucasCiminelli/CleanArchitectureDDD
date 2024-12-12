using AutoFixture;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;

namespace CleanArchitecture.Application.UnitTests.Mock
{
    public static class MockVideoRepository
    {
        public static void AddDataVideoRepository(StreamerDbContext streamerDbContextFake)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var video = fixture.Create<Video>();

            var videos = fixture.CreateMany<Video>().ToList();
            
            videos.Add(fixture.Build<Video>()
                .With(tr => tr.Id, 1)
                .With(tr => tr.CreatedBy, "Lucas")
                .With(tr => tr.Nombre, "Titanic")
                .With(tr => tr.StreamerId, 8001)
                .Create()
            );

            streamerDbContextFake.Videos!.Add(video);
            streamerDbContextFake.Videos!.AddRange(videos);
            streamerDbContextFake.SaveChanges();
        }
    }
}
