using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Notes.Application.Common.Exceptions;
using Notes.Application.Common.Data;
using Notes.Domain;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    public class GetNoteDetailsQueryHandler : BaseQueryHandler, IRequestHandler<GetNoteDetailsQuery, NoteDetailsVm>
    {
        public GetNoteDetailsQueryHandler(INotesDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper) { }

        public async Task<NoteDetailsVm> Handle(GetNoteDetailsQuery request, CancellationToken cancellationToken = default)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var note = await _dbContext.Notes.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if ((note == null) || (note.UserId != request.UserId))
            {
                throw new EntityNotFoundException(nameof(Note), request.Id);
            }

            return _mapper.Map<NoteDetailsVm>(note);
        }
    }
}
