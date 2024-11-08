using BreweryWholesaleMngmnt.Models.DTO;

namespace BreweryWholesaleMngmnt.Services
{
    public interface IQuoteService
    {
        Task<QuoteResponseDTO> RequestQuoteAsync(int clientId, int wholesalerId, List<QuoteItemDTO> items);
    }
}
