using BreweryWholesaleMngmnt.Data;
using BreweryWholesaleMngmnt.Models;
using BreweryWholesaleMngmnt.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesaleMngmnt.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly BreweryContext _context;

        public QuoteService(BreweryContext context)
        {
            _context = context;
        }

        public async Task<QuoteResponseDTO> RequestQuoteAsync(int clientId, int wholesalerId, List<QuoteItemDTO> items)
        {
            // si le client existe
            var client = await ValidateClientAsync(clientId);
            // si le wholesaler existe
            var wholesaler = await ValidateWholesalerAsync(wholesalerId);

            // vérifier les items du devis
            ValidateOrderItems(items);

            var (totalPrice, totalQuantity) = CalculateTotalPriceAndQuantity(wholesaler, items);
            decimal discount = CalculateDiscount(totalQuantity);
            decimal discountedPrice = totalPrice * (1 - discount);

            var quote = await CreateQuoteAsync(clientId, wholesalerId, items, discountedPrice, discount);
            return new QuoteResponseDTO
            {
                TotalPrice = discountedPrice,
                Summary = quote.Items.ToList()
            };
        }

        private async Task<Client> ValidateClientAsync(int clientId)
        {
            var client = await _context.Clients.FindAsync(clientId);
            if (client == null)
            {
                throw new Exception("The client must exist.");
            }
            return client;
        }

        private async Task<Wholesaler> ValidateWholesalerAsync(int wholesalerId)
        {
            var wholesaler = await _context.Wholesalers
                .Include(w => w.WholesalerStocks!)
                .ThenInclude(ws => ws.Beer)
                .FirstOrDefaultAsync(w => w.WholesalerID == wholesalerId);

            if (wholesaler == null)
            {
                throw new Exception("The wholesaler must exist.");
            }

            return wholesaler;
        }

        private void ValidateOrderItems(List<QuoteItemDTO> items)
        {
            if (items == null || !items.Any())
            {
                throw new Exception("The order cannot be empty.");
            }

            if (items.GroupBy(i => i.BeerID).Any(g => g.Count() > 1))
            {
                throw new Exception("There can't be any duplicate in the order.");
            }
        }

        private (decimal totalPrice, int totalQuantity) CalculateTotalPriceAndQuantity(Wholesaler wholesaler, List<QuoteItemDTO> items)
        {
            decimal totalPrice = 0;
            int totalQuantity = 0;

            foreach (var item in items)
            {
                var stockItem = wholesaler.WholesalerStocks!.FirstOrDefault(s => s.BeerID == item.BeerID);
                if (stockItem == null)
                {
                    throw new Exception("The beer must be sold by the wholesaler.");
                }

                if (item.Quantity > stockItem.Quantity)
                {
                    throw new Exception("The number of beers ordered cannot be greater than the wholesaler's stock.");
                }

                decimal pricePerUnit = stockItem.Beer!.Price;
                totalPrice += pricePerUnit * item.Quantity;
                totalQuantity += item.Quantity;
            }

            return (totalPrice, totalQuantity);
        }

        private decimal CalculateDiscount(int totalQuantity)
        {
            if (totalQuantity > 20)
            {
                return 0.20m;
            }
            else if (totalQuantity > 10 && totalQuantity < 20)
            {
                return 0.10m;
            }
            return 0;
        }

        private async Task<Quote> CreateQuoteAsync(int clientId, int wholesalerId, List<QuoteItemDTO> items, decimal discountedPrice, decimal discount)
        {
            var quote = new Quote
            {
                ClientID = clientId,
                WholesalerID = wholesalerId,
                RequestedAt = DateTime.UtcNow,
                TotalPrice = discountedPrice,
                Items = items.Select(i => new QuoteItem
                {
                    BeerID = i.BeerID,
                    Quantity = i.Quantity,
                    Price = _context.Beers.Find(i.BeerID)!.Price * i.Quantity * (1 - discount)
                }).ToList()
            };

            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();

            return quote;
        }

    }
}
