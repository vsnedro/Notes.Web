using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Notes.Application.Notes.Commands.CreateNote;

using Xunit;

namespace Notes.Application.Tests.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandlerTests : BaseCommandHandlerTests
    {
        private readonly CreateNoteCommandHandler _handler;

        public CreateNoteCommandHandlerTests()
        {
            _handler = new CreateNoteCommandHandler(DbContext);
        }

        [Fact]
        public async Task Handle_RequestIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            CreateNoteCommand request = null;

            // Act
            async Task action() => await _handler.Handle(request);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }

        [Fact]
        public async Task Handle_RequestContainsNewNote_DbContextContainsSingleEntity()
        {
            var request = new CreateNoteCommand();
            (request.UserId, request.Title, request.Details) =
                (CommandTestsConstants.UserId, CommandTestsConstants.NoteTitle, CommandTestsConstants.NoteDetails);

            await _handler.Handle(request);

            //DbContext.Notes.Count().ShouldBe(1);
            Assert.Single(DbContext.Notes);
        }

        [Fact]
        public async Task Handle_RequestContainsNewNote_DbContextContainsInsertedEntity()
        {
            var request = new CreateNoteCommand();
            (request.UserId, request.Title, request.Details) =
                (CommandTestsConstants.UserId, CommandTestsConstants.NoteTitle, CommandTestsConstants.NoteDetails);

            var noteId = await _handler.Handle(request);

            Assert.NotNull(await DbContext.Notes.SingleOrDefaultAsync(
                x => x.Id == noteId &&
                x.UserId == CommandTestsConstants.UserId &&
                x.Title == CommandTestsConstants.NoteTitle &&
                x.Details == CommandTestsConstants.NoteDetails &&
                x.CreatedDate.Date == DateTime.Today));
        }
    }
}
