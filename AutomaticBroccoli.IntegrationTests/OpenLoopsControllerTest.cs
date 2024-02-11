using AutomaticBroccoli.API.Contracts;
using AutomaticBroccoli.DataAccess.Models;
using AutomaticBroccoli.DataAccess.Repository;
using System.Net.Http.Json;

namespace AutomaticBroccoli.IntegrationTests
{
    public class OpenLoopsControllerTest : IntegrationTestBase
    {
        [Fact]
        public async Task Get_ShouldReturnOkStatus()
        {
            //Если не статика
            //var repository = factory.Services.GetRequiredService<OpenLoopsRepository>();

            OpenLoopsRepository.Add(new OpenLoop(Guid.NewGuid(), "Test note", DateTimeOffset.UtcNow));

            var response = await Client.GetAsync("/v1/OpenLoops");
            response.EnsureSuccessStatusCode();

            Directory.Delete(OpenLoopsRepository.DataDirecotory, true);
        }

        [Fact]
        public async Task Create_AdditionalOpenLoop_ShouldReturnOkStatus()
        {
            OpenLoopsRepository.Add(new OpenLoop(Guid.NewGuid(), "First open loop", DateTimeOffset.UtcNow));

            var request = new CreateOpenLoopRequest
            {
                Note = "Additional open loop"
            };

            var response = await Client.PostAsJsonAsync("/v1/OpenLoops", request);
            
            response.EnsureSuccessStatusCode();

            Directory.Delete(OpenLoopsRepository.DataDirecotory, true);
        }
    }
}