using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.UpdateNote;

using Xunit;

namespace Notes.Application.Tests.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandHandlerTests : BaseCommandHandlerTests
    {
        private readonly UpdateNoteCommandHandler _handler;

        private static readonly string _noteNewTitle = "NewTitle";
        private static readonly string _noteNewDetails = "NewDetails";

        public UpdateNoteCommandHandlerTests()
        {
            _handler = new UpdateNoteCommandHandler(DbContext);
        }

        [Fact]
        public async Task Handle_RequestIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            UpdateNoteCommand request = null;

            // Act
            async Task action() => await _handler.Handle(request);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }

        [Fact]
        public async Task Handle_DbContextIsEmpty_ThrowsEntityNotFoundException()
        {
            var request = new UpdateNoteCommand();
            (request.Id, request.UserId, request.Title, request.Details) =
                (CommandTestsConstants.NoteId, CommandTestsConstants.UserId, CommandTestsConstants.NoteNewTitle, CommandTestsConstants.NoteNewDetails);

            async Task action() => await _handler.Handle(request);

            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Fact]
        public async Task Handle_RequestContainsWrongNoteId_ThrowsEntityNotFoundException()
        {
            await AddNote();
            var request = new UpdateNoteCommand();
            (request.Id, request.UserId, request.Title, request.Details) =
                (CommandTestsConstants.WrongNoteId, CommandTestsConstants.UserId, CommandTestsConstants.NoteNewTitle, CommandTestsConstants.NoteNewDetails);

            async Task action() => await _handler.Handle(request);

            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Fact]
        public async Task Handle_RequestContainsWrongUserId_ThrowsEntityNotFoundException()
        {
            await AddNote();
            var request = new UpdateNoteCommand();
            (request.Id, request.UserId, request.Title, request.Details) =
                (CommandTestsConstants.NoteId, CommandTestsConstants.WrongUserId, CommandTestsConstants.NoteNewTitle, CommandTestsConstants.NoteNewDetails);

            async Task action() => await _handler.Handle(request);

            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Fact]
        public async Task Handle_RequestContainsNewNote_DbContextContainsUpdatedEntity()
        {
            await AddNote();
            var request = new UpdateNoteCommand();
            (request.Id, request.UserId, request.Title, request.Details) =
                (CommandTestsConstants.NoteId, CommandTestsConstants.UserId, CommandTestsConstants.NoteNewTitle, CommandTestsConstants.NoteNewDetails);

            await _handler.Handle(request);

            Assert.NotNull(await DbContext.Notes.SingleOrDefaultAsync(
                x => x.Id == CommandTestsConstants.NoteId &&
                x.UserId == CommandTestsConstants.UserId &&
                x.Title == CommandTestsConstants.NoteNewTitle &&
                x.Details == CommandTestsConstants.NoteNewDetails &&
                x.ModifiedDate.Value.Date == DateTime.Today));
        }
    }
}
