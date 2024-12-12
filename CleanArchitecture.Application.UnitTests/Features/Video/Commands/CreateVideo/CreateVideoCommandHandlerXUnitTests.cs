using AutoMapper;
using Castle.Core.Logging;
using CleanArchitecture.Application.Features.Videos.Commands.CreateVideo;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mock;
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

namespace CleanArchitecture.Application.UnitTests.Features.Video.Commands.CreateVideo
{
    public class CreateVideoCommandHandlerXUnitTests
    {

        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<CreateVideoCommandHandler>> _logger;
        private readonly IMapper _mapper;

        public CreateVideoCommandHandlerXUnitTests()
        {

            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<CreateVideoCommandHandler>>();

            MockVideoRepository.AddDataVideoRepository(_unitOfWork.Object.StreamerDbContext);

        }


        [Fact]

        public async Task CreateVideoCommand_InputVideo_ReturnsNumber()
        {

            var videoInput = new CreateVideoCommand
            {
                Nombre = "Titanic",
                StreamerId = 8001,

            };

            var handler = new CreateVideoCommandHandler(_unitOfWork.Object, _mapper, _logger.Object);

            var result = await handler.Handle(videoInput, CancellationToken.None);


            result.ShouldBeOfType<int>();
            
        }

    }
}
