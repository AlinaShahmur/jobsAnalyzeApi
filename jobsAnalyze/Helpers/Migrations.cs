using jobsAnalyze.Database;
using Microsoft.EntityFrameworkCore;

namespace jobsAnalyze.Helpers
{
    public static class Migrations
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
        }

    }
}
