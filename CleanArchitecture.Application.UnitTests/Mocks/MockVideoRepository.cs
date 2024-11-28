using AutoFixture;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UnitTests.Mock
{
    public static class MockVideoRepository
    {

        public static Mock<IVideoRepository> GetVideoRepository()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior()); //solucion para el problema de referencias circulares al momento de generar data mock
            var videos = fixture.CreateMany<Video>().ToList();


            var mockRepository = new Mock<IVideoRepository>();
            mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(videos);

            return mockRepository;
        }
    }
}
