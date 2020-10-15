using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TaxManagement.Business.Commands;
using TaxManagement.Core.Enums;
using TaxManagement.Core.Models;

namespace TaxManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<TaxController> _logger;

        public TaxController(ILogger<TaxController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Adds municipalities
        /// </summary>
        /// <remarks>
        /// Municipalities to be added can be passed as list.
        /// </remarks>
        /// <param name="municipalities">Municipalities</param>  
        /// <response code="201">Created</response>  
        /// <response code="400">Bad request</response>
        /// <response code="401">Un authorized</response>
        [HttpPost("municipalities")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> AddMunicipality([Required] IEnumerable<Municipality> municipalities)
        {
            _logger.LogInformation($"{nameof(AddMunicipality)} method invoked");

            if (municipalities?.Any() != true)
                return BadRequest($"{nameof(municipalities)} data is required");

            await _mediator.Send(new AddMunicipalityCommand
            {
                Municipalities = municipalities?.Select(x => x.Name).ToList()
            });

            return StatusCode((int)HttpStatusCode.Created);
        }

        /// <summary>
        /// Adds tax details for municipalities
        /// </summary>
        /// <remarks>
        /// For TaxType choose 
        /// 0 for "Daily Tax", 
        /// 1 for "Monthly Tax", 
        /// 2 for "Yearly Tax"
        /// </remarks>
        /// <param name="taxType">Tax type</param>  
        /// <param name="taxDetails">Tax details</param>  
        /// <response code="201">Created</response>  
        /// <response code="400">Bad request</response>
        /// <response code="401">Un authorized</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> AddOrUpdateTax([Required] TaxType taxType, [Required] TaxDetails taxDetails)
        {
            _logger.LogInformation($"{nameof(AddOrUpdateTax)} method invoked");

            await _mediator.Send(new AddOrUpdateTaxCommand
            {
                Municipality = taxDetails.Municipality,
                Date = taxDetails.Date,
                TaxType = taxType,
                Tax = taxDetails.Tax
            });

            return StatusCode((int)HttpStatusCode.Created);
        }

        /// <summary>
        /// Gets tax details of a municipality
        /// </summary>
        /// <param name="municipality">municipality</param>  
        /// <param name="date">date</param>  
        /// <response code="200">OK</response>  
        /// <response code="400">Bad request</response>
        /// <response code="401">Un authorized</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(decimal))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTax([Required] string municipality, [Required] DateTime date)
        {
            _logger.LogInformation($"{nameof(GetTax)} method invoked");

            return Ok(await _mediator.Send(new TaxQuery
            {
                Municipality = municipality,
                Date = date
            }));
        }
    }
}
