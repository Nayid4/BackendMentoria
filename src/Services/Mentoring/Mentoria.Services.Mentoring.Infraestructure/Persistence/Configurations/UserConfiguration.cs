using Mentoria.Services.Mentoring.Domain.AcademicInformations;
using Mentoria.Services.Mentoring.Domain.PersonalInformations;
using Mentoria.Services.Mentoring.Domain.Roles;
using Mentoria.Services.Mentoring.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Value,
                valor => new IdUser(valor));

            builder.Property(t => t.IdPersonalInformation).HasConversion(
                pel => pel.Value,
                valor => new IdPersonalInformation(valor))
                .IsRequired();

            builder.Property(t => t.IdRole).HasConversion(
                pel => pel.Value,
                valor => new IdRole(valor))
                .IsRequired();

            builder.Property(t => t.IdAcademicInformation).HasConversion(
                pel => pel.Value,
                valor => new IdAcademicInformation(valor))
                .IsRequired();

            builder.Property(t => t.UserName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Password)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne(t => t.PersonalInformation)
                .WithMany()
                .HasForeignKey(t => t.IdPersonalInformation)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.AcademicInformation)
                .WithMany()
                .HasForeignKey(t => t.IdAcademicInformation)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Role)
                .WithMany()
                .HasForeignKey(t => t.IdRole);

            builder.Property(t => t.CreatedAt)
                .IsRequired();

            builder.Property(t => t.UpdateAt)
                .IsRequired();
        }
    }
}
