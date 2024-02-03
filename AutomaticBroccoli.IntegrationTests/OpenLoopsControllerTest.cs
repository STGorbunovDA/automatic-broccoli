using AutomaticBroccoli.DataAccess.Models;
using AutomaticBroccoli.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AutomaticBroccoli.IntegrationTests
{
    public class OpenLoopsControllerTest
    {
        [Fact]
        public async Task Get_ShouldReturnOkStatus()
        {
            var factory = new WebApplicationFactory<Program>();
            var client = factory.CreateClient();
            //Если не статика
            //var repository = factory.Services.GetRequiredService<OpenLoopsRepository>();

            OpenLoopsRepository.Add(new OpenLoop(Guid.NewGuid(), "Test note", DateTimeOffset.UtcNow));

            var response = await client.GetAsync("/v1/OpenLoops");
            response.EnsureSuccessStatusCode();

            Directory.Delete(OpenLoopsRepository.DataDirecotory, true);
        }
    }
}