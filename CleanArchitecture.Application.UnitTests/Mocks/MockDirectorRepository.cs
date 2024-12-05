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
    public static class MockDirectorRepository
    {

        public static void AddDataDirectorRepository(StreamerDbContext streamerDbContextFake)
        {

            var fixture = new Fixture();

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var actor = fixture.Build<Director>()
                .With(d => d.Id, 1111)
                .With(d => d.Nombre, "Lucas")
                .With(d => d.Apellido, "Ciminelli")
                .With(d => d.VideoId, 1)
                .Create();

            streamerDbContextFake.Directores!.Add(actor);
            streamerDbContextFake.SaveChanges();

        }

    }
}
