using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Notes.Application.Common.Data;
using Notes.Application.Common.Mappings;
using Notes.Application.Tests.Common;
using Notes.Domain;

using Xunit;

namespace Notes.Application.Tests.Notes.Queries
{
    public class QueryTestsFixture : IDisposable
    {
        public FakeNotesDbContext DbContext { get; }
        public IMapper Mapper { get; }

        public QueryTestsFixture()
        {
            DbContext = FakeNotesDbContextFactory.Create();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
            });
            Mapper = config.CreateMapper();

            AddNotes();
        }

        public void Dispose()
        {
            DbContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        private async Task AddNotes()
        {
            await DbContext.Notes.AddRangeAsync(
                new Note()
                {
                    Id = QueryTestsConstants.NoteA1Id,
                    UserId = QueryTestsConstants.UserAId,
                    Title = QueryTestsConstants.NoteTitle,
                    Details = QueryTestsConstants.NoteDetails
                },
                new Note()
                {
                    Id = QueryTestsConstants.NoteA2Id,
                    UserId = QueryTestsConstants.UserAId,
                    Title = QueryTestsConstants.NoteTitle,
                    Details = QueryTestsConstants.NoteDetails
                },
                new Note()
                {
                    Id = QueryTestsConstants.NoteB1Id,
                    UserId = QueryTestsConstants.UserBId,
                    Title = QueryTestsConstants.NoteTitle,
                    Details = QueryTestsConstants.NoteDetails
                },
                new Note()
                {
                    Id = QueryTestsConstants.NoteB2Id,
                    UserId = QueryTestsConstants.UserBId,
                    Title = QueryTestsConstants.NoteTitle,
                    Details = QueryTestsConstants.NoteDetails
                }
            );
            await DbContext.SaveChangesAsync();
        }
    }

    [CollectionDefinition("QueryTestsCollection")]
    public class QueryTestsCollection : ICollectionFixture<QueryTestsFixture>
    {
    }
}
