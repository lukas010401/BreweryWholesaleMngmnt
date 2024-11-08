using BreweryWholesaleMngmnt.Models.DTO;
using BreweryWholesaleMngmnt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreweryWholesaleMngmnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;

        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpPost("request-quote")]
        public async Task<IActionResult> RequestQuote([FromBody] QuoteRequestDTO quoteRequest)
        {
            try
            {
                var quote = await _quoteService.RequestQuoteAsync(quoteRequest.ClientID, quoteRequest.WholesalerID, quoteRequest.Items!);
                return Ok(quote);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
