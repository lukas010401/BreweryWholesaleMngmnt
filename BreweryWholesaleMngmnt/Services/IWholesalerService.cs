using BreweryWholesaleMngmnt.Models;

namespace BreweryWholesaleMngmnt.Services
{
    public interface IWholesalerService
    {
        Task<WholesalerStock> UpdateWholesalerStockAsync(int wholesalerId, int beerId, int newQuantity);
    }
}
