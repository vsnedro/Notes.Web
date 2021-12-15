using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Notes.Application.Common.Mappings;
using Notes.Domain;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class NoteLookupDto : IMappableWith<Note>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        //public void Mapping(Profile profile) =>
        //    profile.CreateMap<Note, NoteLookupDto>();
    }
}
