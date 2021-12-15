using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Notes.Application.Notes.Commands.DeleteNote;

namespace Notes.Application.Notes.Commands.DeleteNote
{
    internal class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.UserId).NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
