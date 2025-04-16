

using Mentoria.Services.Mentoring.Domain.AcademicInformations;
using Mentoria.Services.Mentoring.Domain.Careers;
using Mentoria.Services.Mentoring.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Configurations
{
    public class AcademicInformationConfiguration : IEntityTypeConfiguration<AcademicInformation>
    {
        public void Configure(EntityTypeBuilder<AcademicInformation> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Value,
                valor => new IdAcademicInformation(valor));

            builder.Property(t => t.Code)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.IdCareer).HasConversion(
                pel => pel.Value,
                valor => new IdCareer(valor))
                .IsRequired();

            builder.Property(t => t.Cicle)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Expectative)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(t => t.CreatedAt)
                .IsRequired();

            builder.Property(t => t.UpdateAt)
                .IsRequired();
        }
    }
}
