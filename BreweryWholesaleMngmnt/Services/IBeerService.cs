using BreweryWholesaleMngmnt.Models;

namespace BreweryWholesaleMngmnt.Services
{
    public interface IBeerService
    {
        Task<List<Beer>> GetBeersByBreweryAsync(int breweryId);
        Task<Beer> AddBeerAsync(Beer beer);
        Task<bool> DeleteBeerAsync(int beerId);
    }
}
