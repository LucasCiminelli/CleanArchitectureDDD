using AutoMapper;
using Azure.Core;
using Castle.Core.Logging;
using CleanArchitecture.Application.Features.Streamers.Queries.GetStreamerByNombre;
using CleanArchitecture.Application.Features.Streamers.Queries.GetStreamersList;
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

namespace CleanArchitecture.Application.UnitTests.Features.Streamers.Queries
{
    public class GetStreamerByNombreQueryHandlerXUnitTests
    {

        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<GetStreamerByNombreQueryHandler>> _logger;
        private readonly IMapper _mapper;

        public GetStreamerByNombreQueryHandlerXUnitTests()
        {

            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();


            _logger = new Mock<ILogger<GetStreamerByNombreQueryHandler>>();


            MockStreamerRepository.AddDataStreamerRepository(_unitOfWork.Object.StreamerDbContext);

        }

        [Fact]

        public async Task GetStreamerByNombre()
        {
            var request = new GetStreamerByNombreQuery("LucasFlix");
            var handler = new GetStreamerByNombreQueryHandler(_unitOfWork.Object, _mapper, _logger.Object);

            var response = await handler.Handle(request, CancellationToken.None);

            response.Nombre.ShouldBe("LucasFlix");
            response.ShouldBeOfType<StreamersVm>();

        }

    }
}
