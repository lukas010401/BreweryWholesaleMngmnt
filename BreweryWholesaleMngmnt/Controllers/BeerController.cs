using BreweryWholesaleMngmnt.Models;
using BreweryWholesaleMngmnt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BreweryWholesaleMngmnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly IBeerService _beerService;

        public BeerController(IBeerService beerService)
        {
            _beerService = beerService;
        }

        // List all the beers by brewery
        [HttpGet("byBrewery/{breweryId}")]
        public async Task<ActionResult<List<Beer>>> GetBeersByBrewery(int breweryId)
        {
            var beers = await _beerService.GetBeersByBreweryAsync(breweryId);
            if(beers.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(beers);
        }

        // A brewer can add new beer
        [HttpPost]
        public async Task<ActionResult<Beer>> AddBeer(Beer beer)
        {
            var addedBeer = await _beerService.AddBeerAsync(beer);
            return CreatedAtAction(nameof(AddBeer), new { id = addedBeer.BeerID }, addedBeer);
        }

        // A brewer can delete a beer
        [HttpDelete("{beerId}")]
        public async Task<ActionResult> DeleteBeer(int beerId)
        {
            var result = await _beerService.DeleteBeerAsync(beerId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
