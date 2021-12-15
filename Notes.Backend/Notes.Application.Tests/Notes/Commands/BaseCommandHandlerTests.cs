using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Notes.Application.Tests.Common;
using Notes.Domain;

namespace Notes.Application.Tests.Notes.Commands
{
    public class BaseCommandHandlerTests : IDisposable
    {
        protected FakeNotesDbContext DbContext { get; }

        public BaseCommandHandlerTests()
        {
            DbContext = FakeNotesDbContextFactory.Create();
        }

        public void Dispose()
        {
            DbContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        protected async Task AddNote()
        {
            await DbContext.Notes.AddAsync(new Note()
            {
                Id = CommandTestsConstants.NoteId,
                UserId = CommandTestsConstants.UserId,
                Title = CommandTestsConstants.NoteTitle,
                Details = CommandTestsConstants.NoteDetails
            });
            await DbContext.SaveChangesAsync();
        }
    }
}
