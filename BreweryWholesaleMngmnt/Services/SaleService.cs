using BreweryWholesaleMngmnt.Data;
using BreweryWholesaleMngmnt.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesaleMngmnt.Services
{
    public class SaleService : ISaleService
    {
        private readonly BreweryContext _context;

        public SaleService(BreweryContext context)
        {
            _context = context;
        }

        public async Task<Sale> AddSaleOfBeerToWholesalerAsync(int wholesalerId, int beerId, int quantity)
        {
            var wholesaler = await _context.Wholesalers.FindAsync(wholesalerId);
            if(wholesaler == null)
            {
                throw new Exception("Wholesaler not found");
            }

            var beer = await _context.Beers.FindAsync(beerId);
            if (beer == null)
            {
                throw new Exception("Beer not found");
            }

            Sale sale = new Sale()
            {
                WholesalerID = wholesalerId,
                BeerID = beerId,
                Quantity = quantity
            };

            _context.Sales.Add(sale);

            await UpdateWholesalerStockAsync(wholesalerId, beerId, quantity);

            await _context.SaveChangesAsync();
            return sale;
        }

        private async Task UpdateWholesalerStockAsync(int wholesalerId, int beerId, int quantity)
        {
            var stockItem = await _context.WholesalerStocks
                                           .FirstOrDefaultAsync(ws => ws.WholesalerID == wholesalerId && ws.BeerID == beerId);

            if (stockItem == null)
            {
                stockItem = new WholesalerStock
                {
                    WholesalerID = wholesalerId,
                    BeerID = beerId,
                    Quantity = quantity
                };
                _context.WholesalerStocks.Add(stockItem);
            }
            else
            {
                stockItem.Quantity += quantity;
            }
        }
    }
}
