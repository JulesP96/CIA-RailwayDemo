using System.Linq;

namespace Demo.Railway.Api.Scopes
{
    /// <summary>
    /// Scopes for this service.
    /// </summary>
    public static class Light
    {
        /// <summary>
        /// Light.Read scope, used for get endpoints.
        /// </summary>
        public const string Read = "light.read";
        /// <summary>
        /// Light.Write scope, used for create, update and delete endpoints.
        /// </summary>
        public const string Write = "light.write";

        /// <summary>
        /// Returns all scopes.
        /// </summary>
        public static string[] All => typeof(Light)
            .GetFields()
            .Where(field => field.Name != nameof(All))
            .Select(field => field.GetValue(null) as string)
            .ToArray();
    }
}