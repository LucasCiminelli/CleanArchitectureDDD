using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Directors.Queries.Vms;
using CleanArchitecture.Application.Features.Shared.Queries;
using CleanArchitecture.Application.Specifications.Directors;
using CleanArchitecture.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Directors.Queries.PaginationDirector
{
    public class PaginationDirectorsQueryHandler : IRequestHandler<PaginationDirectorsQuery, PaginationVm<DirectorsVm>>
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaginationDirectorsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationVm<DirectorsVm>> Handle(PaginationDirectorsQuery request, CancellationToken cancellationToken)
        {

            var directorSpecificationParams = new DirectorSpecificationParams
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort,
            };

            var spec = new DirectorSpecification(directorSpecificationParams);
            var directors = await _unitOfWork.Repository<Director>().GetAllWithSpec(spec);

            var specCount = new DirectorForCountingSpecification(directorSpecificationParams);
            var totalDirectores = await _unitOfWork.Repository<Director>().CountAsync(specCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalDirectores) / Convert.ToDecimal(request.PageSize));
            var totalPages = Convert.ToInt32(rounded);


            var data = _mapper.Map<IReadOnlyList<Director>, IReadOnlyList<DirectorsVm>>(directors);

            var pagination = new PaginationVm<DirectorsVm>
            {
                Count = totalDirectores,
                Data = data,
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };

            return pagination;

        }
    }
}
