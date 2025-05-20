
using Mentoria.Services.Mentoring.Domain.AcademicInformations;
using Mentoria.Services.Mentoring.Domain.Careers;
using Mentoria.Services.Mentoring.Domain.MentorAssignments;
using Mentoria.Services.Mentoring.Domain.PersonalInformations;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivitiesSolutions;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramUsers;
using Mentoria.Services.Mentoring.Domain.Roles;
using Mentoria.Services.Mentoring.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Mentoria.Services.Mentoring.Application.Data
{
    public interface IApplicationDbContext
    {
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

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
