using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace AutomaticBroccoli.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public abstract class BaseController : ControllerBase
    {

    }
}
