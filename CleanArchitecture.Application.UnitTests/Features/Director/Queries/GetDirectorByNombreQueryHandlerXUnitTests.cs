using AutoMapper;
using Castle.Core.Logging;
using CleanArchitecture.Application.Features.Directors.Queries.GetDirectorByNombre;
using CleanArchitecture.Application.Features.Directors.Queries.GetDirectorList;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideoByNombre;
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

namespace CleanArchitecture.Application.UnitTests.Features.Director.Queries
{
    public class GetDirectorByNombreQueryHandlerXUnitTests
    {

        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<GetDirectorByNombreQueryHandler>> _logger;

        public GetDirectorByNombreQueryHandlerXUnitTests()
        {

            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mappingProfile = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mappingProfile.CreateMapper();

            _logger = new Mock<ILogger<GetDirectorByNombreQueryHandler>>();

            MockDirectorRepository.AddDataDirectorRepository(_unitOfWork.Object.StreamerDbContext);

        }

        [Fact]
        public async Task GetDirectorByNombre()
        {

            var request = new GetDirectorByNombreQuery("Lucas");
            var handler = new GetDirectorByNombreQueryHandler(_unitOfWork.Object,_mapper,_logger.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<DirectorsVm>();

            result.Nombre.ShouldBe("Lucas");
          
        }

    }
}
