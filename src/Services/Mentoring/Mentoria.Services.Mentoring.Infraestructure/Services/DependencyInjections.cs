﻿using Mentoria.Services.Mentoring.Application.Data;
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

            // Calcular la ruta completa de wwwroot
            var rutaWwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documentos");

            // Registrar el servicio con la ruta
            services.AddSingleton<IBlobService>(new LocalBlobServices(rutaWwwroot));

            return services;
        }

    }
}
