using System;

using AutoMapper;

using Notes.Application.Common.Mappings;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.UpdateNote;

namespace Notes.WebApi.Models.Note
{
    public class UpdateNoteDto : IMappableWith<UpdateNoteCommand>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        //public void Mapping(Profile profile) =>
        //    profile.CreateMap<UpdateNoteCommand, UpdateNoteDto>();
    }
}
