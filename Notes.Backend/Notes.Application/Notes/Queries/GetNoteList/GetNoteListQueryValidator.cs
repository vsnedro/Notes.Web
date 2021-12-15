using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    internal class GetNoteListQueryValidator : AbstractValidator<GetNoteListQuery>
    {
        public GetNoteListQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
