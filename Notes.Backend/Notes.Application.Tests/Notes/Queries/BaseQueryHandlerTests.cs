using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Notes.Application.Tests.Common;

using Xunit;

namespace Notes.Application.Tests.Notes.Queries
{
    [Collection("QueryTestsCollection")]
    public class BaseQueryHandlerTests
    {
        protected FakeNotesDbContext DbContext { get; }
        protected IMapper Mapper { get; }

        public BaseQueryHandlerTests(QueryTestsFixture fixture)
        {
            DbContext = fixture.DbContext;
            Mapper = fixture.Mapper;
        }
    }
}
