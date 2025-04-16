using Mentoria.Services.Mentoring.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mentoria.Services.Mentoring.API.Extensions
{
    public static class MigrationDeExtensions
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
              
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
