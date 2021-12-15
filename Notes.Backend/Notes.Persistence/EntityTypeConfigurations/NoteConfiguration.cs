using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Notes.Domain;

namespace Notes.Persistence.EntityTypeConfigurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("Notes");

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.HasIndex(x => x.UserId);

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Title)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.Details).HasMaxLength(1024);
            builder.Property(x => x.CreatedDate).IsRequired();
        }
    }
}
