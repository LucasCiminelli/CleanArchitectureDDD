using CleanArchitecture.Application.Features.Directors.Commands.CreateDirector;
using CleanArchitecture.Application.Features.Directors.Commands.DeleteDirector;
using CleanArchitecture.Application.Features.Directors.Commands.UpdateDirector;
using CleanArchitecture.Application.Features.Directors.Queries.GetDirectorByNombre;
using CleanArchitecture.Application.Features.Directors.Queries.GetDirectorList;
using CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DirectorController : ControllerBase
    {
        private IMediator _mediator;

        public DirectorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "CreateDirector")]
        //[Authorize(Roles = "Administrator")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateDirector([FromBody] CreateDirectorCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpPut(Name = "UpdateDirector")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult> UpdateDirector([FromBody] UpdateDirectorCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}",Name = "DeleteDirector")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult> DeleteDirector(int id)
        {

            var command = new DeleteDirectorCommand {Id = id};

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet("username/{username}", Name ="GetDirector")]
        [ProducesResponseType(typeof(IEnumerable<DirectorsVm>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IEnumerable<DirectorsVm>>> GetDirectors(string username)
        {
            var query = new GetDirectorListQuery(username);
            var directors = await _mediator.Send(query);

            return directors;
        }

        [HttpGet("nombre/{nombre}", Name="GetDirectorByNombre")]
        [ProducesResponseType(typeof(DirectorsVm), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<DirectorsVm>> GetDirectorByNombre(string nombre)
        {
            var query = new GetDirectorByNombreQuery(nombre);
            var director = await _mediator.Send(query);

            return director;
        }


    }
}
