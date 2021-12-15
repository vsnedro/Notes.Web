using System;

namespace Notes.Identity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AuthDbContext dbContext)
        {
            _ = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            dbContext.Database.EnsureCreated();
        }
    }
}
