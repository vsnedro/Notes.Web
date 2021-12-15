using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Tests.Common;

using Shouldly;

using Xunit;

namespace Notes.Application.Tests.Notes.Queries.GetNoteDetails
{
    public class GetNoteDetailsHandlerTests : BaseQueryHandlerTests
    {
        private readonly GetNoteDetailsQueryHandler _handler;

        public GetNoteDetailsHandlerTests(QueryTestsFixture fixture) : base(fixture)
        {
            _handler = new GetNoteDetailsQueryHandler(DbContext, Mapper);
        }

        [Fact]
        public async Task Handle_QueryIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            GetNoteDetailsQuery query = null;

            // Act
            async Task action() => await _handler.Handle(query);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }

        [Fact]
        public async Task Handle_DbContextIsEmpty_ThrowsEntityNotFoundException()
        {
            using var dbContext = FakeNotesDbContextFactory.Create();
            var handler = new GetNoteDetailsQueryHandler(dbContext, Mapper);
            var query = new GetNoteDetailsQuery();
            (query.Id, query.UserId) = (QueryTestsConstants.NoteA1Id, QueryTestsConstants.UserAId);

            async Task action() => await handler.Handle(query);

            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Fact]
        public async Task Handle_QueryContainsWrongNoteId_ThrowsEntityNotFoundException()
        {
            var query = new GetNoteDetailsQuery();
            (query.Id, query.UserId) = (QueryTestsConstants.WrongNoteId, QueryTestsConstants.UserAId);

            async Task action() => await _handler.Handle(query);

            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Fact]
        public async Task Handle_QueryContainsWrongUserId_ThrowsEntityNotFoundException()
        {
            var query = new GetNoteDetailsQuery();
            (query.Id, query.UserId) = (QueryTestsConstants.NoteA1Id, QueryTestsConstants.WrongUserId);

            async Task action() => await _handler.Handle(query);

            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Fact]
        public async Task Handle_QueryContainsCorrectData_ReturnsNoteDetailsVm()
        {
            var query = new GetNoteDetailsQuery();
            (query.Id, query.UserId) = (QueryTestsConstants.NoteA1Id, QueryTestsConstants.UserAId);

            var result = await _handler.Handle(query);

            result.ShouldBeOfType<NoteDetailsVm>();
        }

        [Fact]
        public async Task Handle_QueryContainsCorrectData_ReturnsCorrectNoteDetailsVm()
        {
            var query = new GetNoteDetailsQuery();
            (query.Id, query.UserId) = (QueryTestsConstants.NoteA1Id, QueryTestsConstants.UserAId);

            var result = await _handler.Handle(query);

            result.Id.ShouldBe(query.Id);
            result.Title.ShouldBe(QueryTestsConstants.NoteTitle);
            result.Details.ShouldBe(QueryTestsConstants.NoteDetails);
        }
    }
}
