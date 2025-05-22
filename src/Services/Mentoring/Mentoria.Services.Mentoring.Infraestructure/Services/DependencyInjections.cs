using Mentoria.Services.Mentoring.Application.Data;
using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Infraestructure.Persistence.Repositories;
using Mentoria.Services.Mentoring.Infraestructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mentoria.Services.Mentoring.Domain.Users;
using Mentoria.Services.Mentoring.Application.Storage;
using Mentoria.Services.Mentoring.Infraestructure.Persistence.Storages;
using Mentoria.Services.Mentoring.Domain.Roles;
using Mentoria.Services.Mentoring.Domain.Careers;
using Mentoria.Services.Mentoring.Domain.PersonalInformations;
using Mentoria.Services.Mentoring.Domain.AcademicInformations;
using Mentoria.Services.Mentoring.Application.Email;
using Mentoria.Services.Mentoring.Infraestructure.Persistence.Emails;
using Mentoria.Services.Mentoring.Domain.MentorAssignments;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;

namespace Mentoria.Services.Mentoring.Infraestructure.Services
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.AgregarPersistencias(configuracion);
            return servicios;
        }

        public static IServiceCollection AgregarPersistencias(this IServiceCollection services, IConfiguration configuracion)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuracion.GetConnectionString("Database"), sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);
                }));

            services.AddScoped<IApplicationDbContext>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ICareerRepository, CareerRepository>();
            services.AddScoped<IPersonalInformationRepository, PersonalInformationRepository>();
            services.AddScoped<IAcademicInformationRepository, AcademicInformationRepository>();
            services.AddScoped<IMentorAssignmentRepository, MentorAssignmentRepository>();
            services.AddScoped<IProgramRepository, ProgramRepository>();
            services.AddScoped<IProgramActivityRepository, ProgramActivityRepository>();

            // Calcular la ruta completa de wwwroot
            var rutaWwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documentos");

            // Registrar el servicio con la ruta
            services.AddSingleton<IBlobService>(new LocalBlobServices(rutaWwwroot));
            
            services.AddTransient<IEmailService, EmailService>(sp =>
            {
                var emailConfig = configuracion.GetSection("EmailConfiguration");
                var emailService = new EmailService(
                    emailConfig["From"]!,
                    emailConfig["Password"]!,
                    emailConfig["SmtpServer"]!,
                    int.Parse(emailConfig["Port"]!)
                );
                return emailService;
            });

            return services;
        }

    }
}
