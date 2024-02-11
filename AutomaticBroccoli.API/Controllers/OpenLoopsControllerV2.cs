using AutomaticBroccoli.API.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AutomaticBroccoli.DataAccess.Postgres;
using Microsoft.EntityFrameworkCore;

namespace AutomaticBroccoli.API.Controllers
{
    [Route("v2/[controller]")]
    public class OpenLoopsV2Controller : BaseController
    {
        private readonly ILogger<OpenLoopsController> _logger;
        private readonly AutomaticBroccoliDbContext _automaticBroccoliDbContext;

        public OpenLoopsV2Controller(ILogger<OpenLoopsController> logger, 
            AutomaticBroccoliDbContext automaticBroccoliDbContext)
        {
            _logger = logger;
            _automaticBroccoliDbContext = automaticBroccoliDbContext;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetOpenLoopsResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery]int userId, int offset = 0, int count = 50)
        {
            //var openLoops = _automaticBroccoliDbContext.OpenLoops.ToArray();

            var openLoops = await _automaticBroccoliDbContext
                            .OpenLoops
                            .Include(x => x.User)
                            .AsNoTracking()
                            .Where(x => x.UserId == userId)
                            .Skip(offset)
                            .Take(count)
                            .ToArrayAsync();
            var totalCountOfOpenLoops = await _automaticBroccoliDbContext
                .OpenLoops
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .CountAsync();

            var response = new GetOpenLoopsResponse()
            {
                OpenLoops = openLoops.Select(x => new GetOpenLoopDto
                {
                    Id = x.Id,
                    Note = x.Note,
                    CreatedDate = x.CreatedDate,
                    UserLogin = x.User.Login
                }).ToArray(),
                Total = totalCountOfOpenLoops
            };

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateOpenLoopRequest request)
        {
            //var openLoop = new OpenLoop(Guid.NewGuid(), request.Note, DateTimeOffset.UtcNow);

            var openLoop = new DataAccess.Postgres.Entities.AutomaticBroccoliDbContext.OpenLoop()
            {
                Id = Guid.NewGuid(),
                Note = request.Note,
                CreatedDate = DateTimeOffset.UtcNow,
                UserId = 1
             };
            
            _automaticBroccoliDbContext.Add(openLoop);
            await _automaticBroccoliDbContext.SaveChangesAsync();

            return Ok(openLoop.Id);
        }



    }
}
