using AutomaticBroccoli.API.Contracts;
using AutomaticBroccoli.DataAccess.Models;
using AutomaticBroccoli.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace AutomaticBroccoli.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class OpenLoopController : ControllerBase
    {
        private readonly ILogger<OpenLoopController> _logger;

        public OpenLoopController(ILogger<OpenLoopController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetOpenLoopsResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var openLoops = OpenLoopsRepository.Get();

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
        public async Task<IActionResult> Create([FromBody]CreateOpenLoopRequest request)
        {

            var openLoopId = OpenLoopsRepository.Add(new OpenLoop(Guid.NewGuid(), request.Note, DateTimeOffset.UtcNow));

            return Ok(openLoopId);
        }
    }
}
