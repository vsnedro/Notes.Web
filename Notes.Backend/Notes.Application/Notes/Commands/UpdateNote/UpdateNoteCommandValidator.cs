using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Notes.Application.Notes.Commands.UpdateNote;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    internal class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.UserId).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Details).MaximumLength(1024);
        }
    }
}
