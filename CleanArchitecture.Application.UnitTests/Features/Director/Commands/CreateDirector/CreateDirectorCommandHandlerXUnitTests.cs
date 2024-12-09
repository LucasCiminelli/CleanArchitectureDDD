using AutoMapper;
using Castle.Core.Logging;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Actors.Commands.CreateActor;
using CleanArchitecture.Application.Features.Directors.Commands.CreateDirector;
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

namespace CleanArchitecture.Application.UnitTests.Features.Director.Commands.CreateDirector
{
    public class CreateDirectorCommandHandlerXUnitTests
    {

        private readonly Mock<ILogger<CreateDirectorCommandHandler>> _logger;
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;

        public CreateDirectorCommandHandlerXUnitTests()
        {

            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mappingConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();

            });

            _mapper = mappingConfig.CreateMapper();

            _logger = new Mock<ILogger<CreateDirectorCommandHandler>>();


            MockDirectorRepository.AddDataDirectorRepository(_unitOfWork.Object.StreamerDbContext);
        }


        [Fact]

        public async Task CreateDirectorCommand_InputDirector_ReturnsNumber()
        {

            var directorInput = new CreateDirectorCommand
            {
                Nombre = "Lucas",
                Apellido = "Ciminelli",
                VideoId = 1

            };

            var handler = new CreateDirectorCommandHandler(_logger.Object, _mapper, _unitOfWork.Object);

            var result = await handler.Handle(directorInput, CancellationToken.None);


            result.ShouldBeOfType<int>();
        }
    }
}
