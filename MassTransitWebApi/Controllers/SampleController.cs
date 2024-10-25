using MassTransit;
using MassTransitWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SampleController : ControllerBase
    {
        private readonly IBus _bus;

        public SampleController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SampleMessage message)
        {
            await _bus.Publish(message);
            return Ok();
        }
    }
}
