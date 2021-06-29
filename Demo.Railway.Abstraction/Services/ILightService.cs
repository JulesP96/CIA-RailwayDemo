using System.Linq;
using System.Threading.Tasks;
using Demo.Railway.Abstraction.Repositories.Documents;
using Jpn.Cosmos.Core.Errors;
using Jpn.Utilities.Result.Models;

namespace Demo.Railway.Abstraction.Services
{
    /// <summary>
    /// Interface for Light Service.
    /// </summary>
    public interface ILightService
    {
        /// <summary>
        /// Returns a light.
        /// </summary>
        /// <param name="id">The light Id.</param>
        /// <returns>A <see cref="Result{TData}"/> of <see cref="Light"/></returns>
        /// <remarks>Returns a <see cref="Result{TData}"/> of <see cref="NotFoundError"/> if not found.</remarks>
        Task<Result<Light>> GetLightAsync(string id);

        /// <summary>
        /// List all lights.
        /// </summary>
        /// <returns>A <see cref="IQueryable{T}"/> of <see cref="Light"/>.</returns>
        IQueryable<Light> List();

    }
}