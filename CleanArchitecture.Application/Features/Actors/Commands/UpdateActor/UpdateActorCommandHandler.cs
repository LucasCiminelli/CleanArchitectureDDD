using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Actors.Commands.UpdateActor
{
    public class UpdateActorCommandHandler : IRequestHandler<UpdateActorCommand>
    {

        private readonly ILogger<UpdateActorCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateActorCommandHandler(ILogger<UpdateActorCommandHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<Unit> Handle(UpdateActorCommand request, CancellationToken cancellationToken)
        {
            var actorToUpdate = await _unitOfWork.Repository<Actor>().GetByIdAsync(request.Id);

            if (actorToUpdate == null)
            {
                _logger.LogError($"No se encontro el streamer id {request.Id}");
                throw new NotFoundException(nameof(Actor), request.Id);
            }

            _mapper.Map(request, actorToUpdate, typeof(UpdateActorCommand), typeof(Actor));


            _unitOfWork.Repository<Actor>().UpdateEntity(actorToUpdate);

            await _unitOfWork.Complete();


            _logger.LogInformation($"La operacion fue exitosa actualizando el actor {request.Id}");

            return Unit.Value;

        }
    }
}
