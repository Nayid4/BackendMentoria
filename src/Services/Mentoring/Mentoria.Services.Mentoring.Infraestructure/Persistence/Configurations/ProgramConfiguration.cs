
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Configurations
{
    public class ProgramConfiguration : IEntityTypeConfiguration<Program>
    {
        public void Configure(EntityTypeBuilder<Program> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Value,
                valor => new IdProgram(valor));

            builder.Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Type)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(t => t.MaximumNumberOfParticipants)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasMany(t => t.Users)
                .WithOne()
                .HasForeignKey(t => t.IdProgram)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Activities)
                .WithOne()
                .HasForeignKey(t => t.IdProgram)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(t => t.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(t => t.UpdateAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();
        }
    }
}
