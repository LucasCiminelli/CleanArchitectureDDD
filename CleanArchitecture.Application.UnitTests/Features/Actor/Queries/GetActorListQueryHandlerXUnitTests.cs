using AutoMapper;
using CleanArchitecture.Application.Features.Actors.Queries.GetActorsList;
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

namespace CleanArchitecture.Application.UnitTests.Features.Actor.Queries
{
    public class GetActorListQueryHandlerXUnitTests
    {

        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;

        public GetActorListQueryHandlerXUnitTests()
        {

            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();


            MockActorRepository.AddDataActorRepository(_unitOfWork.Object.StreamerDbContext);

        }

        [Fact]

        public async Task GetActorsList()
        {
            var request = new GetActorListQuery("Lucas");
            var handler = new GetActorListQueryHandler(_unitOfWork.Object, _mapper);

            var result = await handler.Handle(request, CancellationToken.None);


            result.ShouldBeOfType<List<ActorsVm>>();


        }


    }
}
