using AutomaticBroccoli.API.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Net;
using AutomaticBroccoli.DataAccess.Postgres;

namespace AutomaticBroccoli.API.Controllers
{
    [ApiController]
    [Route("v2/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class OpenLoopsControllerV2 : ControllerBase
    {
        private readonly ILogger<OpenLoopsController> _logger;
        private readonly AutomaticBroccoliDbContext _automaticBroccoliDbContext;

        public OpenLoopsControllerV2(ILogger<OpenLoopsController> logger, 
            AutomaticBroccoliDbContext automaticBroccoliDbContext)
        {
            _logger = logger;
            _automaticBroccoliDbContext = automaticBroccoliDbContext;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetOpenLoopsResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetV2()
        {
            var openLoops = _automaticBroccoliDbContext.OpenLoops.ToArray();

            var response = new GetOpenLoopsResponse()
            {
                OpenLoops = openLoops.Select(x => new GetOpenLoopDto
                {
                    Id = x.Id,
                    Note = x.Note,
                    CreatedDate = x.CreatedDate
                }).ToArray()
            };

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateV2([FromBody] CreateOpenLoopRequest request)
        {
            //var openLoop = new OpenLoop(Guid.NewGuid(), request.Note, DateTimeOffset.UtcNow);

            var openLoop = new DataAccess.Postgres.Entities.AutomaticBroccoliDbContext.OpenLoop()
            {
                Id = Guid.NewGuid(),
                Note = request.Note,
                CreatedDate = DateTimeOffset.UtcNow,
                UsertId = 1
             };
            
            _automaticBroccoliDbContext.Add(openLoop);
            await _automaticBroccoliDbContext.SaveChangesAsync();

            return Ok(openLoop.Id);
        }



    }
}
