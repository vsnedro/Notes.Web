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
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler : BaseCommandHandler, IRequestHandler<UpdateNoteCommand>
    {
        public UpdateNoteCommandHandler(INotesDbContext dbContext) : base(dbContext) { }

        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken = default)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var note = await _dbContext.Notes.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if ((note == null) || (note.UserId != request.UserId))
            {
                throw new EntityNotFoundException(nameof(Note), request.Id);
            }
            (note.Title, note.Details, note.ModifiedDate) = (request.Title, request.Details, DateTime.Now);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
