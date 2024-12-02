using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Features.Actors.Commands.UpdateActor;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mock;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Actor.UpdateActor
{
    public class UpdateActorCommandHandlerXUnitTests
    {

        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<UpdateActorCommandHandler>> _logger;

        public UpdateActorCommandHandlerXUnitTests()
        {

            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<UpdateActorCommandHandler>>();


            MockActorRepository.AddDataActorRepository(_unitOfWork.Object.StreamerDbContext);

        }


        [Fact]

        public async Task UpdateActorCommand_ActorInput_ReturnsUnit()
        {

            var actorInput = new UpdateActorCommand
            {
                Id = 1111,
                Nombre = "Lucas",
                Apellido = "Ciminelli"
            };

            var handler = new UpdateActorCommandHandler(_logger.Object, _mapper, _unitOfWork.Object);

            var result = await handler.Handle(actorInput, CancellationToken.None);

            result.ShouldBeOfType<Unit>();

        }
    }
}
