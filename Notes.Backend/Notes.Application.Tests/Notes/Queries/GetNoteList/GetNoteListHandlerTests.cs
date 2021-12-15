using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Application.Tests.Common;

using Shouldly;

using Xunit;

namespace Notes.Application.Tests.Notes.Queries.GetNoteList
{
    public class GetNoteListHandlerTests : BaseQueryHandlerTests
    {
        private readonly GetNoteListQueryHandler _handler;

        public GetNoteListHandlerTests(QueryTestsFixture fixture) : base(fixture)
        {
            _handler = new GetNoteListQueryHandler(DbContext, Mapper);
        }

        [Fact]
        public async Task Handle_QueryIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            GetNoteListQuery query = null;

            // Act
            async Task action() => await _handler.Handle(query);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }

        [Fact]
        public async Task Handle_QueryContainsCorrectData_ReturnsNoteListVm()
        {
            var query = new GetNoteListQuery();
            (query.UserId) = (QueryTestsConstants.UserAId);

            var result = await _handler.Handle(query);

            result.ShouldBeOfType<NoteListVm>();
        }

        [Fact]
        public async Task Handle_QueryContainsCorrectData_ReturnsCorrectNoteCount()
        {
            var query = new GetNoteListQuery();
            (query.UserId) = (QueryTestsConstants.UserAId);

            var result = await _handler.Handle(query);

            result.Notes.Count.ShouldBe(QueryTestsConstants.UserANotesCount);
        }
    }
}
