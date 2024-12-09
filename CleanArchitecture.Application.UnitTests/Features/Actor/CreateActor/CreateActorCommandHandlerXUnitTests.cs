using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Features.Actors.Commands.CreateActor;
using CleanArchitecture.Application.Features.Streamers.Commands;
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

namespace CleanArchitecture.Application.UnitTests.Features.Actor.CreateActor
{
    public class CreateActorCommandHandlerXUnitTests
    {

        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<CreateActorCommandHandler>> _logger;

        public CreateActorCommandHandlerXUnitTests()
        {
           
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<CreateActorCommandHandler>>();


            MockActorRepository.AddDataActorRepository(_unitOfWork.Object.StreamerDbContext);
        }

        [Fact]
        public async Task CreateActorCommand_InputActor_ReturnsNumber()
        {

            var actorInput = new CreateActorCommand
            {

                Nombre = "Lucas",
                Apellido = "Ciminelli",

            };

            var handler = new CreateActorCommandHandler(_logger.Object, _unitOfWork.Object, _mapper);

            var result = await handler.Handle(actorInput, CancellationToken.None);


            result.ShouldBeOfType<int>();
        }
    }
}
