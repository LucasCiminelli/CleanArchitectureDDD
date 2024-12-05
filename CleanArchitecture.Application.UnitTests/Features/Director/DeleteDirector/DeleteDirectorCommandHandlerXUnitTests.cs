using AutoMapper;
using Castle.Core.Logging;
using CleanArchitecture.Application.Features.Directors.Commands.DeleteDirector;
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

namespace CleanArchitecture.Application.UnitTests.Features.Director.DeleteDirector
{
    public class DeleteDirectorCommandHandlerXUnitTests
    {

        private readonly Mock<ILogger<DeleteDirectorCommandHandler>> _logger;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDirectorCommandHandlerXUnitTests()
        {

            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {

                c.AddProfile<MappingProfile>();

            });

            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<DeleteDirectorCommandHandler>>();

            MockDirectorRepository.AddDataDirectorRepository(_unitOfWork.Object.StreamerDbContext);

        }


        [Fact]

        public async Task DeleteDirectorCommand_DirectorInputById_ReturnsUnit()
        {

            var directorInput = new DeleteDirectorCommand
            {
                Id = 1111
            };

            var handler = new DeleteDirectorCommandHandler(_logger.Object, _unitOfWork.Object, _mapper);

            var result = await handler.Handle(directorInput, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
            
        }

    }
}
