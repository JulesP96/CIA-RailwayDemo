using System.Threading.Tasks;
using Demo.Railway.Abstraction.Enums;
using Demo.Railway.Abstraction.Repositories.Documents;
using Jpn.Utilities.Result.Models;

namespace Demo.Railway.Abstraction.Services
{
    /// <summary>
    /// Interface for the gateway service.
    /// </summary>
    public interface IGatewayService
    {
        /// <summary>
        /// Update light's state.
        /// </summary>
        /// <param name="gateway">The <see cref="Gateway"/> that manage the light.</param>
        /// <param name="lightId">The light Id to update.</param>
        /// <param name="state">The <see cref="LightState"/> to set.</param>
        /// <returns>A <see cref="Result{TData}"/> of <see cref="Gateway"/>.</returns>
        Task<Result<Gateway>> SetLightStateAsync(Gateway gateway, string lightId, LightState state);

        /// <summary>
        /// Get a gateway.
        /// </summary>
        /// <param name="gatewayId">The gateway Id.</param>
        /// <returns>A <see cref="Result{TData}"/> of <see cref="Gateway"/>.</returns>
        Task<Result<Gateway>> GetGatewayAsync(string gatewayId);

        /// <summary>
        /// Ensure that the light is in the correct state.
        /// </summary>
        /// <param name="gateway">The <see cref="Gateway"/> that manage the light.</param>
        /// <param name="lightId">The light Id to update.</param>
        /// <param name="wantedState">The <see cref="LightState"/> of the light.</param>
        /// <returns>A <see cref="Result{TData}"/> of <see cref="LightState"/>.</returns>
        Task<Result<LightState>> ValidateStateAsync(Gateway gateway, string lightId, LightState wantedState);

    }
}