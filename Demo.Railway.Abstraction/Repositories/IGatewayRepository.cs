using System.Threading.Tasks;
using Demo.Railway.Abstraction.Repositories.Documents;

namespace Demo.Railway.Abstraction.Repositories
{
    /// <summary>
    /// Interface for repository of <see cref="Gateway"/>
    /// </summary>
    public interface IGatewayRepository
    {
        /// <summary>
        /// Get a gateway from its id.
        /// </summary>
        /// <param name="gatewayId">The gateway Id.</param>
        /// <returns>A <see cref="Gateway"/> if found.</returns>
        Task<Gateway?> GetGatewayAsync(string gatewayId);
    }
}