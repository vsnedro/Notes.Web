using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Notes.Application.Common.Exceptions;
using Notes.Application.Common.Data;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandHandler : BaseCommandHandler, IRequestHandler<DeleteNoteCommand>
    {
        public DeleteNoteCommandHandler(INotesDbContext dbContext) : base(dbContext) { }

        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken = default)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var note = await _dbContext.Notes.FindAsync(new object[] { request.Id }, cancellationToken);
            if ((note == null) || (note.UserId != request.UserId))
            {
                throw new EntityNotFoundException(nameof(Note), request.Id);
            }

            _dbContext.Notes.Remove(note);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
