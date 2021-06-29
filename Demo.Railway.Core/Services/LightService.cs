using System.Linq;
using System.Threading.Tasks;
using Demo.Railway.Abstraction.Repositories;
using Demo.Railway.Abstraction.Repositories.Documents;
using Demo.Railway.Abstraction.Services;
using Jpn.Cosmos.Core.Errors;
using Jpn.Utilities.Result.Models;

namespace Demo.Railway.Core.Services
{
    /// <summary>
    /// Service to manage lights
    /// </summary>
    public class LightService : ILightService
    {
        private readonly ILightRepository _lightRepository;

        /// <summary>
        /// Constructor for <see cref="LightService"/>
        /// </summary>
        public LightService(ILightRepository lightRepository)
        {
            _lightRepository = lightRepository;
        }

        /// <summary>
        /// Returns a light.
        /// </summary>
        /// <param name="id">The light Id.</param>
        /// <returns>A <see cref="Result{TData}"/> of <see cref="Light"/></returns>
        /// <remarks>Returns a <see cref="Result{TData}"/> of <see cref="NotFoundError"/> if not found.</remarks>
        public async Task<Result<Light>> GetLightAsync(string id)
        {
            Light? light = await _lightRepository.GetLightAsync(id);

            return light is not null 
                ? Result<Light>.Success(light) 
                : Result<Light>.Failure(new NotFoundError());
        }

        /// <summary>
        /// List all lights.
        /// </summary>
        /// <returns>A <see cref="IQueryable{T}"/> of <see cref="Light"/>.</returns>
        public IQueryable<Light> List()
        {
            return _lightRepository.ListLights();
        }
    }
}