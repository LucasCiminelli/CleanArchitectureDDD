using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Actors.Commands.DeleteActor;
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

namespace CleanArchitecture.Application.UnitTests.Features.Actor.DeleteActor
{
    public class DeleteActorCommandHandlerXUnitTests
    {

        private readonly Mock<ILogger<DeleteActorCommandHandler>> _logger;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteActorCommandHandlerXUnitTests()
        {

            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<DeleteActorCommandHandler>>();

            MockActorRepository.AddDataActorRepository(_unitOfWork.Object.StreamerDbContext);

        }

        [Fact]

        public async Task DeleteActorCommand_ActorInputById_ReturnsUnit()
        {

            var actorInput = new DeleteActorCommand
            {
                Id = 1111
            };

            var handler = new DeleteActorCommandHandler(_logger.Object, _unitOfWork.Object, _mapper);

            var result = await handler.Handle(actorInput, CancellationToken.None);

            result.ShouldBeOfType<Unit>();

        }

    }
}
