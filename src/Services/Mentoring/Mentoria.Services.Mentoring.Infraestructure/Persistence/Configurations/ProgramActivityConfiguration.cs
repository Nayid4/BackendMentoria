
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Configurations
{
    public class ProgramActivityConfiguration : IEntityTypeConfiguration<ProgramActivity>
    {
        public void Configure(EntityTypeBuilder<ProgramActivity> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Value,
                valor => new IdProgramActivity(valor));

            builder.Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(t => t.State)
                .HasMaxLength(50)
                .IsRequired();


            builder.HasMany(t => t.ProgramActivitySolutions)
                .WithOne()
                .HasForeignKey(t => t.IdProgramActivity)
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
