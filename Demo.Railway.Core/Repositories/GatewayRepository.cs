using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Railway.Abstraction.Repositories;
using Demo.Railway.Abstraction.Repositories.Documents;

namespace Demo.Railway.Core.Repositories
{
    /// <summary>
    /// Repository for <see cref="Gateway"/> documents.
    /// </summary>
    public class GatewayRepository : IGatewayRepository
    {
        /// <summary>
        /// Gateway database
        /// </summary>
        private static readonly List<Gateway> Gateways = new()
        {
            new Gateway
            {
                Id = "1",
                IpAddress = "192.168.0.10",
                LightIds = new List<string>
                {
                    "1",
                    "2"
                },
                Name = "Gateway 1"
            },
            new Gateway
            {
                Id = "2",
                IpAddress = "192.168.0.20",
                LightIds = new List<string>
                {
                    "3",
                },
                Name = "Gateway 2"
            }
        };


        /// <summary>
        /// Get a gateway from its id.
        /// </summary>
        /// <param name="gatewayId">The gateway Id.</param>
        /// <exception cref="ArgumentNullException"><paramref name="gatewayId"/> is a null reference.</exception>
        /// <returns>A <see cref="Gateway"/> if found.</returns>
        public async Task<Gateway?> GetGatewayAsync(string gatewayId)
        {
            if (string.IsNullOrEmpty(gatewayId)) throw new ArgumentNullException(nameof(gatewayId));

            return await Task.FromResult(Gateways.FirstOrDefault(gateway => gateway.Id == gatewayId));
        }
    }
}