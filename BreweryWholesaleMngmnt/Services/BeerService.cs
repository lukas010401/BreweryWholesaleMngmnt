using BreweryWholesaleMngmnt.Data;
using BreweryWholesaleMngmnt.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesaleMngmnt.Services
{
    public class BeerService : IBeerService
    {
        private readonly BreweryContext _context;

        public BeerService(BreweryContext context)
        {
            _context = context;
        }

        public async Task<Beer> AddBeerAsync(Beer beer)
        {
            // if authentication get the brewer by token for example
            var brewery = await _context.Breweries.FindAsync(beer.BreweryID);
            if(brewery == null)
            {
                throw new Exception("Brewery not found");
            }

            _context.Beers.Add(beer);
            await _context.SaveChangesAsync();
            return beer;
        }

        public async Task<bool> DeleteBeerAsync(int beerId)
        {
            var beer = await _context.Beers.FindAsync(beerId);
            if (beer == null)
            {
                throw new Exception("Beer not found");
            }

            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Beer>> GetBeersByBreweryAsync(int breweryId)
        {
            var brewery = await _context.Breweries.FindAsync(breweryId);
            if (brewery == null)
            {
                throw new Exception("Brewery not found");
            }
            return await _context.Beers.Where(b => b.BreweryID == breweryId).Include(b => b.Brewery).ToListAsync();
        }
    }
}
