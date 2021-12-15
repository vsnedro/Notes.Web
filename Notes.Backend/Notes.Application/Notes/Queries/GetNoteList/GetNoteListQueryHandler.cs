using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Notes.Application.Common.Data;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQueryHandler : BaseQueryHandler, IRequestHandler<GetNoteListQuery, NoteListVm>
    {
        public GetNoteListQueryHandler(INotesDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper) { }

        public async Task<NoteListVm> Handle(GetNoteListQuery request, CancellationToken cancellationToken = default)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var notes = await _dbContext.Notes
                .Where(x => x.UserId == request.UserId)
                .ProjectTo<NoteLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new NoteListVm()
            {
                Notes = notes
            };
        }
    }
}
