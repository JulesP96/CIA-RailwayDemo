using System.Collections.Generic;
using Jpn.Cosmos.Core.Documents;

namespace Demo.Railway.Abstraction.Repositories.Documents
{
    /// <summary>
    /// The Gateway document
    /// </summary>
    public class Gateway : DocumentBase
    {
        /// <summary>
        /// IP Address of the gateway.
        /// </summary>
        /// <example>10.0.0.0</example>
        public string? IpAddress { get; set; }

        /// <summary>
        /// Name of the gateway.
        /// </summary>
        /// <example>Kitchen</example>
        public string? Name { get; set; }

        /// <summary>
        /// Ids of the lights attached to the gateway
        /// </summary>
        public IEnumerable<string>? LightIds { get; set; }
    }
}