using BreweryWholesaleMngmnt.Controllers;
using BreweryWholesaleMngmnt.Models;
using BreweryWholesaleMngmnt.Models.DTO;
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
    public class QuoteControllerTests
    {
        private Mock<IQuoteService> _quoteServiceMock;
        private QuoteController _controller;

        [SetUp]
        public void SetUp()
        {
            _quoteServiceMock = new Mock<IQuoteService>();
            _controller = new QuoteController(_quoteServiceMock.Object);
        }

        [Test]
        public async Task RequestQuote_ValidRequest_ReturnsOkResultWithQuoteResponse()
        {
            int clientID = 1;
            int wholesalerID = 1;

            var items = new List<QuoteItemDTO>
            {
                new QuoteItemDTO { BeerID = 1, Quantity = 10 },
                new QuoteItemDTO { BeerID = 2, Quantity = 15 }
            };

            var quoteResponse = new QuoteResponseDTO
            {
                TotalPrice = 48.5m,
                Summary = new List<QuoteItem>
                {
                    new QuoteItem { BeerID = 1, Quantity = 5, Price = 11 },
                    new QuoteItem { BeerID = 2, Quantity = 15, Price = 37.5m }
                }
            };

            _quoteServiceMock.Setup(service => service.RequestQuoteAsync(clientID, wholesalerID, items))
               .ReturnsAsync(quoteResponse);

            QuoteRequestDTO quoteRequest = new QuoteRequestDTO
            {
                ClientID = clientID,   
                WholesalerID = wholesalerID,
                Items = items
            };

            var result = await _controller.RequestQuote(quoteRequest) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var returnedQuoteResponse = result.Value as QuoteResponseDTO;
            Assert.IsNotNull(returnedQuoteResponse);
            Assert.AreEqual(quoteResponse.TotalPrice, returnedQuoteResponse.TotalPrice);
            Assert.AreEqual(quoteResponse.Summary.Count, returnedQuoteResponse.Summary.Count);
        }

        [Test]
        public async Task RequestQuote_EmptyOrder_ReturnsBadRequestWithMessage()
        {
            int clientID = 1;
            int wholesalerID = 1;
            var items = new List<QuoteItemDTO>();

            _quoteServiceMock.Setup(service => service.RequestQuoteAsync(clientID, wholesalerID, items))
                .ThrowsAsync(new Exception("The order cannot be empty."));

            QuoteRequestDTO quoteRequest = new QuoteRequestDTO
            {
                ClientID = clientID,
                WholesalerID = wholesalerID,
                Items = items
            };

            var result = await _controller.RequestQuote(quoteRequest) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task RequestQuote_DuplicateItemsInOrder_ReturnsBadRequestWithMessage()
        {
            int clientID = 1;
            int wholesalerID = 1;
            var items = new List<QuoteItemDTO>
            {
                new QuoteItemDTO { BeerID = 1, Quantity = 5 },
                new QuoteItemDTO { BeerID = 1, Quantity = 10 }
            };

            _quoteServiceMock.Setup(service => service.RequestQuoteAsync(clientID, wholesalerID, items))
                .ThrowsAsync(new Exception("There can't be any duplicate in the order."));


            QuoteRequestDTO quoteRequest = new QuoteRequestDTO
            {
                ClientID = clientID,
                WholesalerID = wholesalerID,
                Items = items
            };

            var result = await _controller.RequestQuote(quoteRequest) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            //var test = result.Value;
        }
    }
}
