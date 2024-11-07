using BreweryWholesaleMngmnt.Models.DTO;
using BreweryWholesaleMngmnt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreweryWholesaleMngmnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholesalerController : ControllerBase
    {
        private readonly IWholesalerService _wholesalerService;

        public WholesalerController(IWholesalerService wholesalerService)
        {
            _wholesalerService = wholesalerService;
        }

        // A wholesaler can update the remaining quantity of a beer in his stock
        [HttpPut]
        [Route("update-stock")]
        public async Task<IActionResult> UpdateWholesalerStock([FromBody] WholesalerStockDTO stockDto)
        {
            try
            {
                var updatedStock = await _wholesalerService.UpdateWholesalerStockAsync(
                    stockDto.WholesalerId,
                    stockDto.BeerId,
                    stockDto.NewQuantity
                );

                return Ok(updatedStock);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
