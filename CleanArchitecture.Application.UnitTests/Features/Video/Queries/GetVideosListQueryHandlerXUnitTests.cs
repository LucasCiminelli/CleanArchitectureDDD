using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mock;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Video.Queries
{
    public class GetVideosListQueryHandlerXUnitTests
    {

        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public GetVideosListQueryHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            mapperConfig.AssertConfigurationIsValid();
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetVideoListTest()
        {

            var handler = new GetVideosListQueryHandler(_unitOfWork.Object,_mapper);

            var request = new GetVideosListQuery("system");

            var result = await handler.Handle(request, CancellationToken.None);


            result.ShouldBeOfType<List<VideosVm>>();

        }

    }
}
