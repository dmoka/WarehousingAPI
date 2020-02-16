using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using KaliGasService.Core.Data.DAO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehousing.API;
using Warehousing.Data.Database;

namespace Warehousing.Testhelpers
{
    public class IntegrationTestBase : IDisposable 
    {
        protected ServiceCollection Services;
        protected ServiceProvider Provider { get; set; }
        protected WarehousingDbContext DbContext { get; set; }
        protected IDbContextTransaction Transaction { get; }


        private readonly IConfiguration _configuration = new ConfigurationBuilder()
            .SetBasePath(GetCurrentDirectoryPath())
            .AddJsonFile("appsettings.integration.json", true, true)
            .AddEnvironmentVariables()
            .Build();

        //Before every test
        public IntegrationTestBase()
        {
            Services = ConfigServicesBasedOnAPIStartup();

            AddCustomDependencies(Services);
            Provider = Services.BuildServiceProvider();
            CreateAndInitDatabase(Services);

            Transaction = DbContext.Database.BeginTransaction();
        }

        private ServiceCollection ConfigServicesBasedOnAPIStartup()
        {
            var startup = new Startup(_configuration);
            var services = new ServiceCollection();
            startup.ConfigureServices(services);

            return services;
        }

        private static string GetCurrentDirectoryPath()
        {
            var uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
            var path = Uri.UnescapeDataString(uri.Path);

            return Path.GetDirectoryName(path);
        }

        private void AddCustomDependencies(ServiceCollection services)
        {
            services.AddDbContext<WarehousingDbContext>(o => o
                .UseSqlite("Data Source=:memory:")
                .EnableSensitiveDataLogging());

            services.AddScoped<ISqlConnectionFactory>(_ => new SqlConnectionFactory(() => DbContext.Database.GetDbConnection(), DatabaseType.SQLite));
        }

        private void CreateAndInitDatabase(ServiceCollection services)
        {
            DbContext = Provider.GetService<WarehousingDbContext>();
            DbContext.Database.OpenConnection();
            DbContext.Database.EnsureCreated();
        }

        //After every test
        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
                Transaction.Dispose();
            }
        }

        public async Task AddToDbContextAsync<T>(params T[] objs) where T : class
        {
            foreach (var obj in objs)
            {
                await DbContext.Set<T>().AddAsync(obj);
            }
            await DbContext.SaveChangesAsync();
        }


        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var mediator = Provider.GetService<IMediator>();
            return mediator.Send(request);
        }

        public async Task SendAsync(IRequest request)
        {
            var mediator = Provider.GetService<IMediator>();
            await mediator.Send(request);
        }
    }

}
