using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.DeleteNote;

using Xunit;

namespace Notes.Application.Tests.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandHandlerTests : BaseCommandHandlerTests
    {
        private readonly DeleteNoteCommandHandler _handler;

        public DeleteNoteCommandHandlerTests()
        {
            _handler = new DeleteNoteCommandHandler(DbContext);
        }

        [Fact]
        public async Task Handle_RequestIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            DeleteNoteCommand request = null;

            // Act
            async Task action() => await _handler.Handle(request);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }

        [Fact]
        public async Task Handle_DbContextIsEmpty_ThrowsEntityNotFoundException()
        {
            var request = new DeleteNoteCommand();
            (request.Id, request.UserId) = (CommandTestsConstants.NoteId, CommandTestsConstants.UserId);

            async Task action() => await _handler.Handle(request);

            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Fact]
        public async Task Handle_RequestContainsWrongNoteId_ThrowsEntityNotFoundException()
        {
            await AddNote();
            var request = new DeleteNoteCommand();
            (request.Id, request.UserId) = (CommandTestsConstants.WrongNoteId, CommandTestsConstants.UserId);

            async Task action() => await _handler.Handle(request);

            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Fact]
        public async Task Handle_RequestContainsWrongUserId_ThrowsEntityNotFoundException()
        {
            await AddNote();
            var request = new DeleteNoteCommand();
            (request.Id, request.UserId) = (CommandTestsConstants.NoteId, CommandTestsConstants.WrongUserId);

            async Task action() => await _handler.Handle(request);

            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Fact]
        public async Task Handle_RequestContainsNewNote_DbContextNotContainsDeletedEntity()
        {
            await AddNote();
            var request = new DeleteNoteCommand();
            (request.Id, request.UserId) = (CommandTestsConstants.NoteId, CommandTestsConstants.UserId);

            await _handler.Handle(request);

            Assert.Null(await DbContext.Notes.SingleOrDefaultAsync(
                x => x.Id == CommandTestsConstants.NoteId &&
                x.UserId == CommandTestsConstants.UserId));
        }
    }
}
