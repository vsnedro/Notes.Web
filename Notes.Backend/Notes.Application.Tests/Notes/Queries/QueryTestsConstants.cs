using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Tests.Notes.Queries
{
    public static class QueryTestsConstants
    {
        public static readonly Guid UserAId = Guid.Parse("{A74A2344-1495-44F4-8B40-E50250E88445}");
        public static readonly Guid NoteA1Id = Guid.Parse("{928B3B07-F642-477D-B13D-296904597612}");
        public static readonly Guid NoteA2Id = Guid.Parse("{2A6179D6-07A4-436F-902C-67B21A5FBE44}");
        public const int UserANotesCount = 2;

        public static readonly Guid UserBId = Guid.Parse("{5E7207FE-8B69-4221-ACB1-D4F29AF23FD0}");
        public static readonly Guid NoteB1Id = Guid.Parse("{AF01D720-84E6-4BFC-9A12-9342A2B8C7B2}");
        public static readonly Guid NoteB2Id = Guid.Parse("{B6AAEF69-045C-4995-8D21-5209F3524A1C}");

        public static readonly Guid WrongNoteId = Guid.Parse("{C9426C72-AAD6-4249-9816-19B27ED09EF7}");
        public static readonly Guid WrongUserId = Guid.Parse("{F0525C8A-8DA1-432D-AC08-9FF6A9E300AF}");

        public const string NoteTitle = "Title";
        public const string NoteDetails = "Details";
    }
}
