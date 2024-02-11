using AutomaticBroccoli.API.Contracts;
using AutomaticBroccoli.DataAccess.Models;
using AutomaticBroccoli.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace AutomaticBroccoli.API.Controllers
{
    public class OpenLoopsController : BaseController
    {
        private readonly ILogger<OpenLoopsController> _logger;

        public OpenLoopsController(ILogger<OpenLoopsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get list of open loops.
        /// </summary>
        /// <remarks>
        /// Allow a user get list of open loops.<br/>
        /// Open loop is short description what  a user worries about.
        /// </remarks>
        /// <returns>List of open loops</returns>
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
        public async Task<IActionResult> Create([FromBody] CreateOpenLoopRequest request)
        {
            var createNoteResult = Note.Create(request.Note);
            if (createNoteResult.IsFailure)
                return BadRequest();

            var openLoopId = OpenLoopsRepository.Add(new OpenLoop(Guid.NewGuid(), createNoteResult.Value, DateTimeOffset.UtcNow));
            return Ok(openLoopId);
        }
    }
}
