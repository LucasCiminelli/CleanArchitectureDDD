using AutoMapper;
using Castle.Core.Logging;
using CleanArchitecture.Application.Features.Actors.Queries.GetActorByNombre;
using CleanArchitecture.Application.Features.Actors.Queries.GetActorsList;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mock;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Actor.Queries
{
    public class GetActorByNombreQueryHandlerXUnitTests
    {

        private readonly IMapper _mapper;
        private readonly Mock<ILogger<GetActorByNombreQueryHandler>> _logger;
        private readonly Mock<UnitOfWork> _unitOfWork;

        public GetActorByNombreQueryHandlerXUnitTests()
        {

            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<GetActorByNombreQueryHandler>>();


            MockActorRepository.AddDataActorRepository(_unitOfWork.Object.StreamerDbContext);

        }

        [Fact]

        public async Task GetActorByNombre()
        {
            var request = new GetActorByNombreQuery("Lucas");

            var handler = new GetActorByNombreQueryHandler(_unitOfWork.Object, _mapper, _logger.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            result.Nombre.ShouldBe("Lucas");
            result.ShouldBeOfType<ActorsVm>();

        }
        
    }
}
