
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivitiesSolutions;
using Mentoria.Services.Mentoring.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Configurations
{
    public class ProgramActivitySolutionConfiguration : IEntityTypeConfiguration<ProgramActivitySolution>
    {
        public void Configure(EntityTypeBuilder<ProgramActivitySolution> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Value,
                valor => new IdProgramActivitySolution(valor));

            builder.Property(t => t.IdProgramActivity).HasConversion(
                pel => pel.Value,
                valor => new IdProgramActivity(valor))
                .IsRequired();

            builder.Property(t => t.IdUser).HasConversion(
                pel => pel.Value,
                valor => new IdUser(valor))
                .IsRequired();

            builder.HasOne(t => t.User)
                .WithMany() // o .WithMany(u => u.ProgramUsers) si tienes la colección en User
                .HasForeignKey(t => t.IdUser)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(t => t.Calification)
                .IsRequired();

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
