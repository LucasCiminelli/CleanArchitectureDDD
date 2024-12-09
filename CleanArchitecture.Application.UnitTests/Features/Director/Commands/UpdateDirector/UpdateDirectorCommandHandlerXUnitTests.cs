using AutoMapper;
using Castle.Core.Logging;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Directors.Commands.UpdateDirector;
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

namespace CleanArchitecture.Application.UnitTests.Features.Director.Commands.UpdateDirector
{
    public class UpdateDirectorCommandHandlerXUnitTests
    {

        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<UpdateDirectorCommandHandler>> _logger;

        public UpdateDirectorCommandHandlerXUnitTests()
        {

            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<UpdateDirectorCommandHandler>>();

            MockDirectorRepository.AddDataDirectorRepository(_unitOfWork.Object.StreamerDbContext);

        }

        [Fact]

        public async Task UpdateDirectorCommand_DirectorInput_ReturnsUnit()
        {

            var directorToUpdate = new UpdateDirectorCommand
            {
                Id = 1111,
                Nombre = "Lucas",
                Apellido = "Ciminelli",
                VideoId = 1
            };

            var handler = new UpdateDirectorCommandHandler(_unitOfWork.Object, _mapper, _logger.Object);

            var result = await handler.Handle(directorToUpdate, CancellationToken.None);


            result.ShouldBeOfType<Unit>();
        }

    }
}
