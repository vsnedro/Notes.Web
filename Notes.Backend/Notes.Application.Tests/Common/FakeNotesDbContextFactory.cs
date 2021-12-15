using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Notes.Persistence;

namespace Notes.Application.Tests.Common
{
    public static class FakeNotesDbContextFactory
    {
        public static FakeNotesDbContext Create()
        {
            var options = new DbContextOptionsBuilder<NotesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContext = new FakeNotesDbContext(options);
            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
