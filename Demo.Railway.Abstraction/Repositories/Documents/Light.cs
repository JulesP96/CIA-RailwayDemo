using Jpn.Cosmos.Core.Documents;

namespace Demo.Railway.Abstraction.Repositories.Documents
{
    /// <summary>
    /// <see cref="DocumentBase"/> for Light.
    /// </summary>
    public class Light : DocumentBase
    {
        /// <summary>
        /// Name of the light.
        /// </summary>
        /// <example>Ambiance light n°1</example>
        public string? Name { get; set; }

        /// <summary>
        /// Id of the gateway.
        /// </summary>
        /// <example>85ed3d57-10f8-4192-8e4e-6368b2eedba1</example>
        public string? GatewayId { get; set; }
    }
}