using System;
using System.Linq;
using System.Threading.Tasks;
using Demo.Railway.Abstraction.Repositories.Documents;

namespace Demo.Railway.Abstraction.Repositories
{
    /// <summary>
    /// Interface for repository of <see cref="Light"/>.
    /// </summary>
    public interface ILightRepository
    {
        /// <summary>
        /// Get a light from its id.
        /// </summary>
        /// <param name="lightId">The light Id.</param>
        /// <exception cref="ArgumentNullException"><paramref name="lightId"/> is a null reference.</exception>
        /// <returns>A <see cref="Light"/> if found.</returns>
        Task<Light?> GetLightAsync(string lightId);

         /// <summary>
        /// Returns all lights.
        /// </summary>
        /// <returns>A <see cref="IQueryable{T}"/> of <see cref="Light"/>.</returns>
        IQueryable<Light> ListLights();
    }
}