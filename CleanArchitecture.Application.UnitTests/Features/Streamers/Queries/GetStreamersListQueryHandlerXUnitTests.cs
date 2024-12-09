using AutoMapper;
using Castle.Core.Logging;
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
    public class GetStreamersListQueryHandlerXUnitTests
    {

        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<GetStreamersListQueryHandler>> _logger;
        private readonly IMapper _mapper;

        public GetStreamersListQueryHandlerXUnitTests()
        {

            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<GetStreamersListQueryHandler>>();

            MockStreamerRepository.AddDataStreamerRepository(_unitOfWork.Object.StreamerDbContext);

        }


        [Fact]

        public async Task GetDirectorsList()
        {
            var request = new GetStreamersListQuery("Lucas");
            var handler = new GetStreamersListQueryHandler(_unitOfWork.Object, _mapper, _logger.Object);

            var response = await handler.Handle(request, CancellationToken.None);

            response.ShouldBeOfType<List<StreamersVm>>();
            response.Count.ShouldBe(1);
        }

    }
}
