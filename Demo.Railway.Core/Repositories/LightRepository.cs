using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Railway.Abstraction.Repositories;
using Demo.Railway.Abstraction.Repositories.Documents;

namespace Demo.Railway.Core.Repositories
{
    /// <summary>
    /// Repository for <see cref="Light"/> documents.
    /// </summary>
    public class LightRepository : ILightRepository
    {
        /// <summary>
        /// Lights database
        /// </summary>
        private static readonly List<Light> Lights = new()
        {
            new Light {Id = "1", GatewayId = "1", Name = "Light 1"},
            new Light {Id = "2", GatewayId = "1", Name = "Light 2"},
            new Light {Id = "3", GatewayId = "2", Name = "Light 3"}
        };

        /// <summary>
        /// Get a light from its id.
        /// </summary>
        /// <param name="lightId">The light Id.</param>
        /// <exception cref="ArgumentNullException"><paramref name="lightId"/> is a null reference.</exception>
        /// <returns>A <see cref="Light"/> if found.</returns>
        public async Task<Light?> GetLightAsync(string lightId)
        {
            if (string.IsNullOrEmpty(lightId)) throw new ArgumentNullException(nameof(lightId));

            return await Task.FromResult(Lights.FirstOrDefault(light => light.Id == lightId));
        }

        /// <summary>
        /// Returns all lights.
        /// </summary>
        /// <returns>A <see cref="IQueryable{T}"/> of <see cref="Light"/>.</returns>
        public IQueryable<Light> ListLights()
        {
            return Lights.AsQueryable();
        }
    }
}