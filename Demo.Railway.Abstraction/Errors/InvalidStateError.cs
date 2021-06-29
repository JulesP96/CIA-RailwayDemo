using System.Globalization;
using System.Net;
using Jpn.Utilities.Result.Models;

namespace Demo.Railway.Abstraction.Errors
{
    /// <summary>
    /// Indicate an invalid state after sending a command.
    /// </summary>
    public class InvalidStateError : Error  
    {
        /// <summary>
        /// Get a 500 error.
        /// </summary>
        /// <returns><see cref="HttpStatusCode"/> 500.</returns>
        public override HttpStatusCode ToHttpCode() => HttpStatusCode.InternalServerError;

        /// <summary>
        /// Constructor for <see cref="InvalidStateError"/>.
        /// </summary>
        public InvalidStateError()
        {
            this.Message = string.Format(CultureInfo.CurrentCulture, ErrorMessages.InvalidStateError);
        }

    }
}