using AutoMapper;
using CleanArchitecture.Application.Features.Directors.Queries.GetDirectorList;
using CleanArchitecture.Application.Features.Directors.Queries.Vms;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mock;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
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
    public class GetDirectorListQueryHandlerXUnitTests
    {

        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;

        public GetDirectorListQueryHandlerXUnitTests()
        {

            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {

                c.AddProfile<MappingProfile>();

            });

            _mapper = mapperConfig.CreateMapper();


            MockDirectorRepository.AddDataDirectorRepository(_unitOfWork.Object.StreamerDbContext);

        }

        [Fact]

        public async Task GetDirectorsList()
        {

            var request = new GetDirectorListQuery("Lucas");
            var handler = new GetDirectorListQueryHandler(_unitOfWork.Object, _mapper);

            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<List<DirectorsVm>>();
            result.Count.ShouldBe(1);

        }

    }
}
