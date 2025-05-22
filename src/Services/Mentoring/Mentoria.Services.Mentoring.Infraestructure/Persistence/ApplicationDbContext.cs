
using Mentoria.Services.Mentoring.Application.Data;
using Mentoria.Services.Mentoring.Domain.AcademicInformations;
using Mentoria.Services.Mentoring.Domain.Careers;
using Mentoria.Services.Mentoring.Domain.MentorAssignments;
using Mentoria.Services.Mentoring.Domain.PersonalInformations;
using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivitiesSolutions;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramUsers;
using Mentoria.Services.Mentoring.Domain.Roles;
using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
    {

        private readonly IPublisher _publisher;

        public DbSet<User> User { get; set; }
        public DbSet<PersonalInformation> PersonalInformation { get; set; }
        public DbSet<AcademicInformation> AcademicInformation { get; set; }
        public DbSet<Career> Career { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<MentorAssignment> MentorAssignment { get; set; }
        public DbSet<Program> Program { get; set; }
        public DbSet<ProgramUser> ProgramUser { get; set; }
        public DbSet<ProgramActivity> ProgramActivity { get; set; }
        public DbSet<ProgramActivitySolution> ProgramActivitySolution { get; set; }

        public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
        {
            _publisher = publisher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var eventosDeDominio = ChangeTracker.Entries<AggregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.GetDomainEvents().Any())
                .SelectMany(e => e.GetDomainEvents());

            var resultado = await base.SaveChangesAsync(cancellationToken);

            foreach (var evento in eventosDeDominio)
            {
                await _publisher.Publish(evento, cancellationToken);
            }

            return resultado;
        }
    }
}
