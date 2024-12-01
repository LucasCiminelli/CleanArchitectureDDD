using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Actors.Commands.CreateActor
{
    public class CreateActorCommandHandler : IRequestHandler<CreateActorCommand, int>
    {

        private readonly ILogger<CreateActorCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateActorCommandHandler(ILogger<CreateActorCommandHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task<int> Handle(CreateActorCommand request, CancellationToken cancellationToken)
        {
            var actorEntity = _mapper.Map<Actor>(request);

            _unitOfWork.Repository<Actor>().AddEntity(actorEntity);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                _logger.LogError("No se insertó el record de Actor");
                throw new Exception("No se insertó el record de Actor");
            }

            return actorEntity.Id;
        }
    }
}
