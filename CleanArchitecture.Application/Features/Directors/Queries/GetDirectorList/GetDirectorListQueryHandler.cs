using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Directors.Queries.GetDirectorList
{
    public class GetDirectorListQueryHandler : IRequestHandler<GetDirectorListQuery, List<DirectorsVm>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDirectorListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<DirectorsVm>> Handle(GetDirectorListQuery request, CancellationToken cancellationToken)
        {
           var directorsList = await _unitOfWork.DirectorRepository.GetDirectorsByUsername(request._Username);

            return _mapper.Map<List<DirectorsVm>>(directorsList);
        }
    }
}
