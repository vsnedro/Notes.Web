using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Notes.Application.Common.Data;

namespace Notes.Persistence.DependencyInjection
{
    public static class PersistenceServiceCollectionExtensions
    {
        private const string DefaultDbConnectionName = "DbConnection";

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration[DefaultDbConnectionName];
            services.AddDbContext<NotesDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
            services.AddScoped<INotesDbContext>(provider => provider.GetService<NotesDbContext>());

            return services;
        }
    }
}
