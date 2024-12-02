using AutoFixture;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public class MockActorRepository
    {

        public static void AddDataActorRepository(StreamerDbContext streamerDbContextFake)
        {

            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var actors = fixture.CreateMany<Actor>().ToList();

            actors.Add(fixture.Build<Actor>()
                .With(actor => actor.Id, 1111)
                .With(actor => actor.Nombre, "Lucas")
                .With(actor => actor.Apellido, "Ciminelli")
                .Without(actor => actor.Videos)
                .Create()
            );

            streamerDbContextFake.Actores!.AddRange(actors);
            streamerDbContextFake.SaveChanges();

        }

    }
}
