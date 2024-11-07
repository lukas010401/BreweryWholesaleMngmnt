using BreweryWholesaleMngmnt.Models;
using BreweryWholesaleMngmnt.Models.DTO;
using BreweryWholesaleMngmnt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreweryWholesaleMngmnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        // Add the sale of an existing beer to an existing wholesaler
        [HttpPost]
        public async Task<ActionResult<Sale>> AddSale([FromBody]SaleDTO saleDto)
        {
            try
            {
                var sale = await _saleService.AddSaleOfBeerToWholesalerAsync(
                    saleDto.WholesalerId,
                    saleDto.BeerId,
                    saleDto.Quantity
                );

                return CreatedAtAction(nameof(AddSale), new { id = sale.SaleID }, sale);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
