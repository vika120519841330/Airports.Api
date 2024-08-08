using Airports.BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airports.Api.Controllers
{
    [ApiController]
    [Route("airports")]
    public class AirportsController : ControllerBase
    {
        private readonly AirportsService airportsService;

        public AirportsController(AirportsService airportsService)
        {
            this.airportsService = airportsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(decimal), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetCard([FromQuery] string first, [FromQuery] string second, [FromHeader] CancellationToken token = default)
        {
            var distance = await airportsService.GetDistance(first, second, token);
            if (distance is not null)
                return Ok(distance);
            else return BadRequest("Incorrect request parameters");
        }
    }
}
