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

namespace CleanArchitecture.Application.Features.Actors.Commands.DeleteActor
{
    public class DeleteActorCommandHandler : IRequestHandler<DeleteActorCommand>
    {

        private readonly ILogger<DeleteActorCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IMapper _mapper;

        public DeleteActorCommandHandler(ILogger<DeleteActorCommandHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task<Unit> Handle(DeleteActorCommand request, CancellationToken cancellationToken)
        {
            var actorToDelete = await _unitOfWork.Repository<Actor>().GetByIdAsync(request.Id);

            if (actorToDelete == null)
            {
                _logger.LogError($"El actor con id {request.Id} no existe");
                throw new ArgumentException($"El actor con id {request.Id} no existe");

            }

            _unitOfWork.Repository<Actor>().DeleteEntity(actorToDelete);
            await _unitOfWork.Complete();

            _logger.LogInformation($"El actor con id {actorToDelete.Id} fue eliminado exitosamente");

            return Unit.Value;

        }
    }
}
