using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TaxManagement.API.Controllers;
using TaxManagement.Business.Commands;
using TaxManagement.Core.Models;
using Xunit;

namespace TaxManagement.Tests
{
    public class TaxManagementTests
    {
        Mock<IMediator> mockMediator;
        Mock<ILogger<TaxController>> mockLogger;
        TaxController taxController;

        public TaxManagementTests()
        {
            mockMediator = new Mock<IMediator>();
            mockLogger = new Mock<ILogger<TaxController>>();
            taxController = new TaxController(mockLogger.Object, mockMediator.Object);
        }
        
        [Fact]
        public async Task AddMunicipality_Created()
        {
            mockMediator.Setup(x => x.Send(It.IsAny<AddMunicipalityCommand>(), default(System.Threading.CancellationToken))).Verifiable();

            var payload = new List<Municipality> { new Municipality { Name = "Copenhagen" } };

            var result =  (await taxController.AddMunicipality(payload)) as StatusCodeResult;

            Assert.Equal(result.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        public async Task AddMunicipality_BadRequest()
        {
            var result = (await taxController.AddMunicipality(null)) as BadRequestObjectResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddOrUpdateTax_Created()
        {
            mockMediator.Setup(x => x.Send(It.IsAny<AddOrUpdateTaxCommand>(), default(System.Threading.CancellationToken))).Verifiable();

            var taxDetails = new TaxDetails
            {
                Municipality = "Copenhagen",
                Tax = 1
            };

            var result = (await taxController.AddOrUpdateTax(0, taxDetails)) as StatusCodeResult;

            Assert.Equal(result.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        public async Task GetTax_OK()
        {
            mockMediator.Setup(x => x.Send(It.IsAny<TaxQuery>(), default(System.Threading.CancellationToken))).ReturnsAsync(1);

            var result = (await taxController.GetTax("Copenhagen", DateTime.Now)) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(result.StatusCode, (int)HttpStatusCode.OK);
        }
    }
}
