using CleanArchitecture.Application.Features.Videos.Commands.CreateVideo;
using CleanArchitecture.Application.Features.Videos.Commands.DeleteVideo;
using CleanArchitecture.Application.Features.Videos.Commands.UpdateVideo;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideoByNombre;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VideoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("username/{username}", Name = "GetVideo")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<VideosVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<VideosVm>>> GetVideosByUsername(string username)
        {
            var query = new GetVideosListQuery(username);
            var videos = await _mediator.Send(query);
            return Ok(videos);
        }

        [HttpGet("nombre/{nombre}", Name= "GetVideoByNombre")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<VideosVm>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IEnumerable<VideosVm>>> GetVideoByNombre(string nombre)
        {
            var query = new GetVideoByNombreQuery(nombre);
            var videos = await _mediator.Send(query);
            return Ok(videos);
        }


        [HttpPost(Name ="CreateVideo")]
        [ProducesResponseType((int)HttpStatusCode.OK)]

        public async Task<ActionResult<int>> CreateVideo([FromBody] CreateVideoCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpPut(Name = "UpdateVideo")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult> UpdateVideo([FromBody] UpdateVideoCommand command)
        {
            await _mediator.Send(command);


            return NoContent();
        }


        [HttpDelete("{id}", Name = "DeleteVideo")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult> DeleteVideo(int id)
        {
            var command = new DeleteVideoCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }



    }

}
