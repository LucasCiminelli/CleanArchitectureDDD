using AutoMapper;
using Castle.Core.Logging;
using CleanArchitecture.Application.Features.Videos.Commands.DeleteVideo;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mock;
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

namespace CleanArchitecture.Application.UnitTests.Features.Video.Commands.DeleteVideo
{
    public class DeleteVideoCommandHandlerXUnitTests
    {

        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<DeleteVideoCommandHandler>> _logger;
        private readonly IMapper _mapper;

        public DeleteVideoCommandHandlerXUnitTests()
        {

            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<DeleteVideoCommandHandler>>();

            MockVideoRepository.AddDataVideoRepository(_unitOfWork.Object.StreamerDbContext);

        }


        [Fact]

        public async Task DeleteVideoCommand_InputStreamerId_ReturnsUnit()
        {
            var command = new DeleteVideoCommand
            {
                Id = 1
            };

            var handler = new DeleteVideoCommandHandler(_unitOfWork.Object, _mapper, _logger.Object);

            var result = await handler.Handle(command, CancellationToken.None);


            result.ShouldBeOfType<Unit>();

        }

    }
}
