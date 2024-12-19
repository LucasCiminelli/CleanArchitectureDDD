using AutoMapper;
using Castle.Core.Logging;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideoByNombre;
using CleanArchitecture.Application.UnitTests.Mock;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Domain;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.Features.Videos.Queries.Vms;

namespace CleanArchitecture.Application.UnitTests.Features.Video.Queries
{
    public class GetVideoByNombreQueryHandlerXUnitTest
    {

        
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<GetVideoByNombreQueryHandler>> _logger;
        private readonly IMapper _mapper;

        public GetVideoByNombreQueryHandlerXUnitTest()
        {

            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c => {

                c.AddProfile<MappingProfile>();

            });

            _mapper = mapperConfig.CreateMapper();

            MockVideoRepository.AddDataVideoRepository(_unitOfWork.Object.StreamerDbContext);

            _logger = new Mock<ILogger<GetVideoByNombreQueryHandler>>();
        }



        [Fact]
        public async Task GetVideoByNombreTest()
        {
            var handler = new GetVideoByNombreQueryHandler(_logger.Object, _unitOfWork.Object, _mapper);
            var request = new GetVideoByNombreQuery("Titanic");

            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<VideosVm>();
            result.Nombre.ShouldBe("Titanic");
        }
    }
}
