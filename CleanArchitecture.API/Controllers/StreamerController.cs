using CleanArchitecture.Application.Features.Streamers.Commands;
using CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Application.Features.Streamers.Queries.GetStreamerByNombre;
using CleanArchitecture.Application.Features.Streamers.Queries.GetStreamersList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers
{
    
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StreamerController : ControllerBase
    {

        private IMediator _mediator;

        public StreamerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpPost(Name = "CreateStreamer")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateStreamer([FromBody] CreateStreamerCommand command)
        {
          return  await  _mediator.Send(command);
        }

        [HttpPut(Name = "UpdateStreamer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateStreamer([FromBody] UpdateStreamerCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }


        [HttpDelete("{id}", Name = "DeleteStreamer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteStreamer(int id)
        {
            var command = new DeleteStreamerCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();    
        }


        [HttpGet("username/{username}", Name = "GetStreamer")]
        [ProducesResponseType(typeof(IEnumerable<StreamersVm>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IEnumerable<StreamersVm>>> GetStreamersList(string username)
        {
            var query = new GetStreamersListQuery(username);
            var streamers = await _mediator.Send(query);
            return Ok(streamers);

        }

        [HttpGet("nombre/{nombre}", Name = "GetStreamerByName")]
        [ProducesResponseType(typeof(StreamersVm), (int)HttpStatusCode.OK)]

        public async Task <ActionResult<StreamersVm>> GetStreamerByNombre(string nombre)
        {
            var query = new GetStreamerByNombreQuery(nombre);
            var streamer = await _mediator.Send(query);
            return Ok(streamer);
        }

    }
}
