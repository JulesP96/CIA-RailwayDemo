using System;
using System.Threading.Tasks;
using Demo.Railway.Abstraction.Enums;
using Demo.Railway.Abstraction.Errors;
using Demo.Railway.Abstraction.Repositories;
using Demo.Railway.Abstraction.Repositories.Documents;
using Demo.Railway.Abstraction.Services;
using Jpn.Cosmos.Core.Errors;
using Jpn.Utilities.Result.Models;

namespace Demo.Railway.Core.Services
{
    /// <summary>
    /// Service for managing <see cref="Gateway"/>.
    /// </summary>
    public class GatewayService : IGatewayService
    {
        private readonly IGatewayRepository _gatewayRepository;

        /// <summary>
        /// Constructor for <see cref="GatewayService"/>.
        /// </summary>
        /// <param name="gatewayRepository">The <see cref="IGatewayRepository"/>.</param>
        public GatewayService(IGatewayRepository gatewayRepository)
        {
            _gatewayRepository = gatewayRepository;
        }

        /// <summary>
        /// Update light's state.
        /// </summary>
        /// <param name="gateway">The <see cref="Gateway"/> that manage the light.</param>
        /// <param name="lightId">The light Id to update.</param>
        /// <param name="state">The <see cref="LightState"/> to set.</param>
        /// <returns>A <see cref="Result{TData}"/> of <see cref="Gateway"/>.</returns>

        public async Task<Result<Gateway>> SetLightStateAsync(Gateway gateway, string lightId, LightState state)
        {
            return await Task.FromResult(Result<Gateway>.Success(gateway));
        }

        /// <summary>
        /// Ensure that the light is in the correct state.
        /// </summary>
        /// <param name="gateway">The <see cref="Gateway"/> that manage the light.</param>
        /// <param name="lightId">The light Id to update.</param>
        /// <param name="wantedState">The <see cref="LightState"/> of the light.</param>
        /// <returns>A <see cref="Result{TData}"/> of <see cref="LightState"/>.</returns>
        public async Task<Result<LightState>> ValidateStateAsync(Gateway gateway, string lightId, LightState wantedState)
        {
            return wantedState switch
            {
                LightState.On => await Task.FromResult(Result<LightState>.Success(LightState.On)),
                LightState.Off => await Task.FromResult(Result<LightState>.Failure(new InvalidStateError())),
                _ => throw new ArgumentOutOfRangeException(nameof(wantedState), wantedState, null)
            };
        }

        /// <summary>
        /// Get a gateway.
        /// </summary>
        /// <param name="gatewayId">The gateway Id.</param>
        /// <returns>A <see cref="Result{TData}"/> of <see cref="Gateway"/>.</returns>
        public async Task<Result<Gateway>> GetGatewayAsync(string gatewayId)
        {
            Gateway? gateway = await _gatewayRepository.GetGatewayAsync(gatewayId);

            return gateway is not null
                ? Result<Gateway>.Success(gateway)
                : Result<Gateway>.Failure(new NotFoundError());
        }
    }
}