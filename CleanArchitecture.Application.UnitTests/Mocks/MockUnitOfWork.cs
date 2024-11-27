using CleanArchitecture.Application.Contracts.Persistence;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UnitTests.Mock
{
    public static class MockUnitOfWork
    {

        public static Mock<IUnitOfWork> GetUnitOfWork()
        {

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var mockVideoRepository = MockVideoRepository.GetVideoRepository();

            mockUnitOfWork.Setup(r => r.VideoRepository).Returns(mockVideoRepository.Object);


            return mockUnitOfWork;  
        }


    }
}
