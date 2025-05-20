
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramUsers;
using Mentoria.Services.Mentoring.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Configurations
{
    public class ProgramUserConfiguration : IEntityTypeConfiguration<ProgramUser>
    {
        public void Configure(EntityTypeBuilder<ProgramUser> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Value,
                valor => new IdProgramUser(valor));

            builder.Property(t => t.IdProgram).HasConversion(
                pel => pel.Value,
                valor => new IdProgram(valor))
                .IsRequired();

            builder.Property(t => t.IdUser).HasConversion(
                pel => pel.Value,
                valor => new IdUser(valor))
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
