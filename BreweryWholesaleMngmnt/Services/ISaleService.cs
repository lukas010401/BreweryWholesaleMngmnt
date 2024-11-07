using BreweryWholesaleMngmnt.Models;

namespace BreweryWholesaleMngmnt.Services
{
    public interface ISaleService
    {
        Task<Sale> AddSaleOfBeerToWholesalerAsync(int wholesalerId, int beerId, int quantity);
    }
}
