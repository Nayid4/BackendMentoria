
using Mentoria.Services.Mentoring.Domain.MentorAssignments;
using Mentoria.Services.Mentoring.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Configurations
{
    public class MentorAssignmentConfiguration : IEntityTypeConfiguration<MentorAssignment>
    {
        public void Configure(EntityTypeBuilder<MentorAssignment> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Value,
                valor => new IdMentorAssignment(valor));

            builder.Property(t => t.IdMentor).HasConversion(
                pel => pel.Value,
                valor => new IdUser(valor))
                .IsRequired();

            builder.Property(t => t.IdUser).HasConversion(
                pel => pel.Value,
                valor => new IdUser(valor))
                .IsRequired();

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.IdUser)
                .OnDelete(DeleteBehavior.NoAction); // o .NoAction()

            builder.HasOne(t => t.Mentor)
                .WithMany()
                .HasForeignKey(t => t.IdMentor)
                .OnDelete(DeleteBehavior.NoAction); // o .NoAction()


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
