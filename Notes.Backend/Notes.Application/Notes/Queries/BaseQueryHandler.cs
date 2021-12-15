using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Notes.Application.Common.Data;

namespace Notes.Application.Notes.Queries
{
    public abstract class BaseQueryHandler
    {
        protected INotesDbContext _dbContext;
        protected IMapper _mapper;

        public BaseQueryHandler(INotesDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
