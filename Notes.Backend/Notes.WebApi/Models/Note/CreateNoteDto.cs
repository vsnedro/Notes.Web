using System.ComponentModel.DataAnnotations;

using AutoMapper;

using Notes.Application.Common.Mappings;
using Notes.Application.Notes.Commands.CreateNote;

namespace Notes.WebApi.Models.Note
{
    public class CreateNoteDto : IMappableWith<CreateNoteCommand>
    {
        [Required]
        public string Title { get; set; }

        public string Details { get; set; }

        //public void Mapping(Profile profile) =>
        //    profile.CreateMap<CreateNoteCommand, CreateNoteDto>();
    }
}
