using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Notes.Domain;

namespace Notes.Application.Common.Data
{
    public interface INotesDbContext
    {
        public DbSet<Note> Notes { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken token);
    }
}
