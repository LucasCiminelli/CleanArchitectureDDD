using AutoMapper;
using Castle.Core.Logging;
using CleanArchitecture.Application.Features.Videos.Commands.UpdateVideo;
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

namespace CleanArchitecture.Application.UnitTests.Features.Video.Commands.UpdateVideo
{
    public class UpdateVideoCommandHandlerXUnitTests
    {

        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<UpdateVideoCommandHandler>> _logger;
        private readonly IMapper _mapper;

        public UpdateVideoCommandHandlerXUnitTests()
        {

            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<UpdateVideoCommandHandler>>();

            MockVideoRepository.AddDataVideoRepository(_unitOfWork.Object.StreamerDbContext);

        }

        [Fact]

        public async Task UpdateVideoCommand_InputVideo_ReturnsUnit()
        {

            var inputVideo = new UpdateVideoCommand
            {
                Id = 1,
                Nombre = "Titanic",
                StreamerId = 8001
            };

            var handler = new UpdateVideoCommandHandler(_unitOfWork.Object, _logger.Object, _mapper);

            var result = await handler.Handle(inputVideo, CancellationToken.None);

            result.ShouldBeOfType<Unit>();

        }

    }
}
