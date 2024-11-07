using BreweryWholesaleMngmnt.Controllers;
using BreweryWholesaleMngmnt.Models.DTO;
using BreweryWholesaleMngmnt.Models;
using BreweryWholesaleMngmnt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreweryWholesale.Tests
{
    [TestFixture]
    public class SaleControllerTests
    {
        private SaleController _controller;
        private Mock<ISaleService> _mockSaleService;

        [SetUp]
        public void Setup()
        {
            _mockSaleService = new Mock<ISaleService>();
            _controller = new SaleController(_mockSaleService.Object);
        }

        [Test]
        public async Task AddSale_ReturnsCreatedAtAction_WhenSaleIsAddedSuccessfully()
        {
            var saleDto = new SaleDTO { WholesalerId = 1, BeerId = 2, Quantity = 10 };
            var sale = new Sale { SaleID = 1, WholesalerID = 1, BeerID = 2, Quantity = 10 };

            _mockSaleService
                .Setup(service => service.AddSaleOfBeerToWholesalerAsync(saleDto.WholesalerId, saleDto.BeerId, saleDto.Quantity))
                .ReturnsAsync(sale);

            var result = await _controller.AddSale(saleDto);

            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(nameof(_controller.AddSale), createdResult.ActionName);
            Assert.AreEqual(sale, createdResult.Value);
        }

    }
}
