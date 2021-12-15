using System;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebApi.Models.Note;

namespace Notes.WebApi.Controllers.Note
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    //[ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/[controller]")]
    public class NoteController : BaseController
    {
        private readonly IMapper _mapper;

        public NoteController(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets the note by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /note/7E43DCC6-D348-4758-BB03-DE2C87090CC9
        /// </remarks>
        /// <param name="id">Id of the note (GUID).</param>
        /// <returns>Returns NoteDetailsVm.</returns>
        /// <response code="200">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async ValueTask<ActionResult<NoteDetailsVm>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery()
            {
                Id = id,
                UserId = UserId,
            };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of notes.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /note
        /// </remarks>
        /// <returns>Returns NoteListVm.</returns>
        /// <response code="200">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async ValueTask<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery()
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /note
        /// {
        ///     title: "Note title",
        ///     details: "Note details"
        /// }
        /// </remarks>
        /// <param name="createNoteDto">CreateNoteDto object.</param>
        /// <returns>Returns note id (GUID).</returns>
        /// <response code="201">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async ValueTask<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
        {
            var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
            command.UserId = UserId;
            var noteId = await Mediator.Send(command);

            return Ok(noteId);
        }

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /note
        /// {
        ///     title: "New note title",
        /// }
        /// </remarks>
        /// <param name="updateNoteDto">UpdateNoteDto object.</param>
        /// <returns>Returns NoContent.</returns>
        /// <response code="204">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        /// <response code="404">If the note does not exist.</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async ValueTask<ActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
        {
            var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
            command.UserId = UserId;
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Deletes the note by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /note/7E43DCC6-D348-4758-BB03-DE2C87090CC9
        /// </remarks>
        /// <param name="id">Id of the note (GUID).</param>
        /// <returns>Returns NoContent.</returns>
        /// <response code="204">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        /// <response code="404">If the note does not exist.</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async ValueTask<ActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand()
            {
                Id = id,
                UserId = UserId
            };
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
