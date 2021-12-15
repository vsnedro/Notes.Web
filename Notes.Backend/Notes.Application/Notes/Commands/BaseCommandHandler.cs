using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Notes.Application.Common.Data;

namespace Notes.Application.Notes.Commands
{
    public abstract class BaseCommandHandler
    {
        protected INotesDbContext _dbContext;

        public BaseCommandHandler(INotesDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
