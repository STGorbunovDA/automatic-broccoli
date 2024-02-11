using AutoFixture;
using AutomaticBroccoli.API.Contracts;
using Npgsql;
using Respawn;
using Respawn.Graph;
using System.Net.Http.Json;
using static AutomaticBroccoli.DataAccess.Postgres.Entities.AutomaticBroccoliDbContext;

namespace AutomaticBroccoli.IntegrationTests
{
    public class OpenLoopsV2ControllerTest : IntegrationTestBase
    {
        [Fact]
        public async Task Get_ShouldReturnOkStatus()
        {
            try
            {
                var fixture = new Fixture();
                var newOpenLoops = fixture.Build<OpenLoop>()
                    .Without(x => x.User)
                    .Without(x => x.UserId)
                    .Without(x => x.CreatedDate)
                    .CreateMany(42)
                    .ToArray();

                var user = new User
                {
                    Login = "test@mail.com",
                    OpenLoops = newOpenLoops
                };

                Context.Users.Add(user);
                await Context.SaveChangesAsync();
                Context.ChangeTracker.Clear();

                var openLoops = await GetOpenLoops(user.Id);
                Assert.NotNull(openLoops?.OpenLoops);
                Assert.NotEmpty(openLoops.OpenLoops);
                Assert.Equal(42, openLoops.Total);
            }
            finally
            {
                await CleanAsync();
            }
        }

        [Fact]
        public async Task Get_InvalidUserId_ShouldReturnOkStatusWithEmptyResult()
        {
            var userId = 0;
            var openLoops = await GetOpenLoops(userId);
            Assert.NotNull(openLoops?.OpenLoops);
            Assert.Empty(openLoops.OpenLoops);
            Assert.True(openLoops.Total == 0);
        }

        private async Task<GetOpenLoopsResponse?> GetOpenLoops(int userId)
        {
            var response = await Client.GetAsync($"/v2/OpenLoopsV2?userId={userId}");
            Assert.NotNull(response);
            response.EnsureSuccessStatusCode();

            var result = await response!.Content!.ReadFromJsonAsync<GetOpenLoopsResponse>();
            return result;
        }

        private async Task CleanAsync()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            await connection.OpenAsync();
            var respawner = await Respawner.CreateAsync(
                connection, new RespawnerOptions
                {
                    TablesToIgnore = new Table[]
                    {
                        "__EFMigrationsHistory"
                    },
                    SchemasToInclude = new[]
                    {
                        "public"
                    },
                    DbAdapter = DbAdapter.Postgres
                });

            await respawner.ResetAsync(connection);
        }
    }
}