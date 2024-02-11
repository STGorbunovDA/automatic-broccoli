using AutomaticBroccoli.DataAccess.Postgres;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutomaticBroccoli.IntegrationTests
{
    public abstract class IntegrationTestBase : IDisposable
    {
        protected HttpClient Client {  get; }

        private readonly IServiceScope _scope;

        protected AutomaticBroccoliDbContext Context { get; }
        protected string? ConnectionString { get; }

        public IntegrationTestBase() 
        {
            var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .AddUserSecrets<IntegrationTestBase>()
                        .Build();

            ConnectionString = configuration.GetConnectionString("DefaultConnection");

            var factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseConfiguration(configuration);
                });
            Client = factory.CreateClient();
            _scope = factory.Services.CreateScope();

            Context = _scope.ServiceProvider.GetRequiredService<AutomaticBroccoliDbContext>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}