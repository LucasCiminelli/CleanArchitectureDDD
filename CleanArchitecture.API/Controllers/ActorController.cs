using CleanArchitecture.Application.Features.Actors.Commands.CreateActor;
using CleanArchitecture.Application.Features.Actors.Commands.DeleteActor;
using CleanArchitecture.Application.Features.Actors.Commands.UpdateActor;
using CleanArchitecture.Application.Features.Actors.Queries.GetActorByNombre;
using CleanArchitecture.Application.Features.Actors.Queries.GetActorsList;
using CleanArchitecture.Application.Features.Actors.Queries.PaginationActor;
using CleanArchitecture.Application.Features.Actors.Queries.Vms;
using CleanArchitecture.Application.Features.Shared.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ActorController : ControllerBase
    {

        private IMediator _mediator;

        public ActorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "CreateActor")]
        [ProducesResponseType((int)HttpStatusCode.OK)]

        public async Task<ActionResult<int>> CreateActor([FromBody] CreateActorCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpPut(Name = "UpdateActor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult> UpdateActor([FromBody] UpdateActorCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteActor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult> DeleteActor(int id)
        {
            var command = new DeleteActorCommand { Id = id };

            await _mediator.Send(command);

            return NoContent();

        }


        [HttpGet("username/{username}", Name = "GetActors")]
        [ProducesResponseType(typeof(IEnumerable<ActorsVm>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IEnumerable<ActorsVm>>> GetActors(string username)
        {
            var query = new GetActorListQuery(username);
            var actors = await _mediator.Send(query);

            return actors;
        }

        [HttpGet("nombre/{nombre}", Name = "GetActorByNombre")]
        [ProducesResponseType(typeof(ActorsVm), (int)HttpStatusCode.OK)]
        
        public async Task<ActionResult<ActorsVm>> GetActorByNombre(string nombre)
        {
            var query = new GetActorByNombreQuery(nombre);
            var actor = await _mediator.Send(query);

            return actor;
        }

        [HttpGet("pagination", Name = "PaginationActor")]
        [ProducesResponseType(typeof(PaginationVm<ActorsVm>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<PaginationVm<ActorsVm>>> GetPaginationActor([FromQuery] PaginationActorQuery paginationActorQuery)
        {
            var paginationActor = await _mediator.Send(paginationActorQuery);

            return Ok(paginationActor);
        }

    }
}
