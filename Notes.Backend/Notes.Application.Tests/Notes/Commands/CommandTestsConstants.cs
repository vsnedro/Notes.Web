using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Tests.Notes.Commands
{
    public static class CommandTestsConstants
    {
        public static readonly Guid UserId = Guid.Parse("{A74A2344-1495-44F4-8B40-E50250E88445}");
        public static readonly Guid NoteId = Guid.Parse("{928B3B07-F642-477D-B13D-296904597612}");

        public static readonly Guid WrongNoteId = Guid.Parse("{C9426C72-AAD6-4249-9816-19B27ED09EF7}");
        public static readonly Guid WrongUserId = Guid.Parse("{F0525C8A-8DA1-432D-AC08-9FF6A9E300AF}");

        public const string NoteTitle = "Title";
        public const string NoteDetails = "Details";

        public const string NoteNewTitle = "NewTitle";
        public const string NoteNewDetails = "NewDetails";
    }
}
