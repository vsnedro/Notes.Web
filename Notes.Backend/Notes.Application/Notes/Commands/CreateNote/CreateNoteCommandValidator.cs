using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace Notes.Application.Notes.Commands.CreateNote
{
    internal class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Details).MaximumLength(1024);
        }
    }
}
