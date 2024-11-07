using BreweryWholesaleMngmnt.Controllers;
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
    public class BeerControllerTests
    {
        private Mock<IBeerService> _beerServiceMock;
        private BeerController _controller;

        [SetUp]
        public void SetUp()
        {
            _beerServiceMock = new Mock<IBeerService>();
            _controller = new BeerController(_beerServiceMock.Object);
        }
        
        // there are beers
        [Test]
        public async Task GetBeersByBrewery_ReturnsOkResult_WhenBeersExists()
        {
            var breweryId = 1;
            var beers = new List<Beer>
            {
                new Beer { BeerID = 1, Name = "Beer 1", BreweryID = breweryId },
                new Beer { BeerID = 2, Name = "Beer 2", BreweryID = breweryId }
            };

            _beerServiceMock.Setup(service => service.GetBeersByBreweryAsync(breweryId)).ReturnsAsync(beers);

            var result = await _controller.GetBeersByBrewery(breweryId);

            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);
            var returnBeers = okResult?.Value as List<Beer>;
            Assert.NotNull(returnBeers);
            Assert.AreEqual(2, returnBeers.Count);
        }

        [Test]
        public async Task GetBeersByBrewery_ReturnsNotFoundResult_WhenNoBeersExist()
        {
            var breweryId = 1;
            _beerServiceMock.Setup(service => service.GetBeersByBreweryAsync(breweryId))
                            .ReturnsAsync(new List<Beer>());

            var result = await _controller.GetBeersByBrewery(breweryId);

            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task AddBeer_ReturnsCreatedAtAction_WhenBeerIsAdded()
        {
            var beer = new Beer { BeerID = 1, Name = "New Beer", BreweryID = 1 };
            _beerServiceMock.Setup(service => service.AddBeerAsync(beer))
                            .ReturnsAsync(beer);

            var result = await _controller.AddBeer(beer);

            var createdResult = result.Result as CreatedAtActionResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual("AddBeer", createdResult?.ActionName);
            var addedBeer = createdResult?.Value as Beer;
            Assert.NotNull(addedBeer);
            Assert.AreEqual(beer.Name, addedBeer?.Name);
        }

        [Test]
        public async Task DeleteBeer_ReturnsNoContent_WhenBeerIsDeleted()
        {
            var beerId = 1;
            _beerServiceMock.Setup(service => service.DeleteBeerAsync(beerId))
                            .ReturnsAsync(true);

            var result = await _controller.DeleteBeer(beerId);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteBeer_ReturnsNotFound_WhenBeerDoesNotExist()
        {
            var beerId = 1;
            _beerServiceMock.Setup(service => service.DeleteBeerAsync(beerId))
                            .ReturnsAsync(false);

            var result = await _controller.DeleteBeer(beerId);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
