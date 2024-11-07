using BreweryWholesaleMngmnt.Data;
using BreweryWholesaleMngmnt.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesaleMngmnt.Services
{
    public class WholesalerService : IWholesalerService
    {
        private readonly BreweryContext _context;

        public WholesalerService(BreweryContext context)
        {
            _context = context;
        }

        public async Task<WholesalerStock> UpdateWholesalerStockAsync(int wholesalerId, int beerId, int newQuantity)
        {
            var wholesaler = await _context.Wholesalers.FindAsync(wholesalerId);
            if (wholesaler == null)
            {
                throw new Exception("Wholesaler not found");
            }

            var beer = await _context.Beers.FindAsync(beerId);
            if (beer == null)
            {
                throw new Exception("Beer not found");
            }

            var stock = await _context.WholesalerStocks
                                   .FirstOrDefaultAsync(ws => ws.WholesalerID == wholesalerId && ws.BeerID == beerId);

            if (stock == null)
            {
                stock = new WholesalerStock()
                {
                    WholesalerID = wholesalerId,
                    BeerID = beerId,
                    Quantity = newQuantity
                };

                _context.WholesalerStocks.Add(stock);
            }

            else stock.Quantity = newQuantity;
            await _context.SaveChangesAsync();
            return stock;
        }
    }
}
