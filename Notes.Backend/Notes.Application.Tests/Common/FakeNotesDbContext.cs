using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Notes.Persistence;

namespace Notes.Application.Tests.Common
{
    public class FakeNotesDbContext : NotesDbContext
    {
        private bool _disposed = false;

        public FakeNotesDbContext(DbContextOptions<NotesDbContext> options) : base(options)
        {
        }

        #region IDisposable
        public override void Dispose()
        {
            if (_disposed) return;

            Database.EnsureDeleted();
            _disposed = true;

            base.Dispose();

            GC.SuppressFinalize(this);
        }

        public override async ValueTask DisposeAsync()
        {
            if (_disposed) return;

            await base.DisposeAsync();

            Dispose();
        }
        #endregion
    }
}
