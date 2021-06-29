using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Demo.Railway.Abstraction.Enums;
using Demo.Railway.Abstraction.Repositories.Documents;
using Demo.Railway.Abstraction.Services;
using Jpn.Utilities.AspNetCore.Result.Extensions;
using Jpn.Utilities.Result.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Demo.Railway.Core.Extensions;

namespace Demo.Railway.Api.Controllers
{
    /// <summary>
    /// Controller for <see cref="Light"/>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class LightController : ControllerBase
    {
        private readonly ILightService _lightService;
        private readonly IGatewayService _gatewayService;
        private readonly ILogger<LightController> _logger;

        /// <summary>
        /// Initializes a new <see cref="LightController"/>.
        /// </summary>
        /// <param name="lightService">The service to manage lights.</param>
        /// <param name="gatewayService">The service to manage gateways.</param>
        /// <param name="logger">The <see cref="ILogger{T}"/>.</param>
        public LightController(
            ILightService lightService,
            IGatewayService gatewayService,
            ILogger<LightController> logger)
        {
            _lightService = lightService;
            _gatewayService = gatewayService;
            _logger = logger;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <remarks>
        /// List all lights.
        /// </remarks>
        /// <response code="200">OK - Returns all lights.</response>
        [ProducesResponseType(typeof(List<Light>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public IActionResult List()
        {
            var query = _lightService.List();

            return Ok(query.ToList());
        }

        /// <summary>
        /// Get light
        /// </summary>
        /// <remarks>
        /// Get a light by its id.
        /// </remarks>
        /// <response code="200">OK - Returns light.</response>
        [ProducesResponseType(typeof(Light), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetLight(string id)
        {
            return await _lightService.GetLightAsync(id)
                .HandleAsync(this);
        }

        /// <summary>
        /// Get gateway
        /// </summary>
        /// <remarks>
        /// Get a gateway by light id.
        /// </remarks>
        /// <response code="200">OK - Returns gateway.</response>
        [ProducesResponseType(typeof(Gateway), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("{id}/gateway")]
        public async Task<IActionResult> GetGateway(string id)
        {
            return await _lightService.GetLightAsync(id)
                .OnSuccessAsync(light => _gatewayService.GetGatewayAsync(light.GatewayId!))
                .HandleAsync(this);
        }

        /// <summary>
        /// Set light's state 
        /// </summary>
        /// <remarks>
        /// Update light's state [on | off]
        /// </remarks>
        /// <response code="200">OK - Returns light current state.</response>
        [ProducesResponseType(typeof(LightState), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> SendCommand(string id, [FromBody] LightState state)
        {
            var light = await _lightService.GetLightAsync(id);

            return await light
                .OnSuccessAsync(_ => _gatewayService.GetGatewayAsync(_.GatewayId!))
                .OnSuccessAsync(_ => _gatewayService.SetLightStateAsync(_, id, state))
                .OnBothAsync(
                    onSuccess: () => _logger.LogInformation($"[{nameof(LightController)}] - Sent {state} to {light.Data.Name}"),
                    onError: () => _logger.LogWarning($"[{nameof(LightController)}] - Failed to sent command to light id: {id}"))
                .OnSuccessAsync(gateway => _gatewayService.ValidateStateAsync(gateway, id, state))
                .TeeAsync(_ => _logger.LogInformation($"[{nameof(LightController)}] - Light is in the state: {_}"))
                .HandleAsync(this);
        }

        [Obsolete]
        private async Task<IActionResult> SendCommandObsolete(string id, [FromBody] LightState state)
        {
            // Getting light
            var light = (await _lightService.GetLightAsync(id)).Data;
            if (light is null) return NotFound();

            // Getting light's gateway
            var gateway = (await _gatewayService.GetGatewayAsync(light.GatewayId!)).Data;
            if (gateway is null) return NotFound();

            _logger.LogInformation($"[{nameof(LightController)}] - Sent {state} to {light.Name}");
            await _gatewayService.SetLightStateAsync(gateway, id, state);

            try
            {
                var currentState = await _gatewayService.ValidateStateAsync(gateway, id, state);
                _logger.LogInformation($"[{nameof(LightController)}] - Light is in the state: {currentState}");
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            return Ok(state);
        }
    }
}
