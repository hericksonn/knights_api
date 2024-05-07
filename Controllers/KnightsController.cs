using KnightsApi.Models;
using KnightsApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KnightsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KnightsController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;

        public KnightsController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<Knight>> Get() => await _mongoDBService.GetAllKnightsAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Knight>> Get(string id)
        {
            var knight = await _mongoDBService.GetKnightAsync(id);
            if (knight == null)
            {
                return NotFound();
            }
            return knight;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Knight newKnight)
        {
            await _mongoDBService.CreateKnightAsync(newKnight);
            return CreatedAtAction(nameof(Get), new { id = newKnight.Id }, newKnight);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Knight updatedKnight)
        {
            await _mongoDBService.UpdateKnightAsync(id, updatedKnight);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteKnightAsync(id);
            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] string? filter)
        {
            if (filter == "heroes")
            {
                var heroes = await _mongoDBService.GetHeroesAsync();
                return Ok(heroes);
            }

            var knights = await _mongoDBService.GetAllKnightsAsync();
            return Ok(knights);
        }
    }
}
