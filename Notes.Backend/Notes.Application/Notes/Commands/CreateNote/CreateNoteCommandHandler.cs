using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Notes.Application.Common.Data;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler : BaseCommandHandler, IRequestHandler<CreateNoteCommand, Guid>
    {
        public CreateNoteCommandHandler(INotesDbContext dbContext) : base(dbContext) { }

        public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken = default)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var note = new Note()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now
            };
            (note.UserId, note.Title, note.Details) = (request.UserId, request.Title, request.Details);

            await _dbContext.Notes.AddAsync(note, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return note.Id;
        }
    }
}
