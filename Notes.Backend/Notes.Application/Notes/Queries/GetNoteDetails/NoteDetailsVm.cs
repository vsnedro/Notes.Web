using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Notes.Application.Common.Mappings;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Domain;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    public class NoteDetailsVm : IMappableWith<Note>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        //public void Mapping(Profile profile) =>
        //    profile.CreateMap<Note, NoteDetailsVm>();
    }
}
