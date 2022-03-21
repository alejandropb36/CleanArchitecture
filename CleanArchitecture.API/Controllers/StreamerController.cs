using CleanArchitecture.Application.Features.Streamers.Commands;
using CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StreamerController: ControllerBase
    {
        private readonly IMediator _mediator;

        public StreamerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "CreateStreamer")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateStreamer([FromBody] CreateStreamerCommand createStreamerCommand)
        {
            return await _mediator.Send(createStreamerCommand);
        }

        [HttpPut(Name = "UpdateStreamer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateStreamer([FromBody] UpdateStreamerCommand updateStreamerCommand)
        {
            await _mediator.Send(updateStreamerCommand);
            return NoContent();
        }

        [HttpDelete("{id}",Name = "DeleteStreamer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteStreamer(int id)
        {
            var deleteStreamerCommand = new DeleteStreamerCommand
            {
                Id = id
            };
            await _mediator.Send(deleteStreamerCommand);
            return NoContent();
        }
    }
}
