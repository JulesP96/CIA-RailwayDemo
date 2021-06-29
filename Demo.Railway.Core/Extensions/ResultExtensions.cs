using System;
using System.Threading.Tasks;
using Jpn.Utilities.Result.Models;

namespace Demo.Railway.Core.Extensions
{
    /// <summary>
    /// Extensions for <see cref="Result{TData}"/>.
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Execute <paramref name="onSuccess"/> or <paramref name="onError"/> based on the Result.
        /// </summary>
        /// <param name="task">The <see cref="Result{TData}"/> task.</param>
        /// <param name="onSuccess">Action to execute on success.</param>
        /// <param name="onError">Action to execute on error.</param>
        /// <typeparam name="T">Result type.</typeparam>
        /// <returns>A <see cref="Result{TData}"/>.</returns>
        public static async Task<Result<T>> OnBothAsync<T>(
            this Task<Result<T>> task,
            Action onSuccess,
            Action onError)
        {
            var result = await task;
            if (result.IsSuccess())
            {
                onSuccess();
                return Result<T>.Success(result.Data);
            }
            
            onError();
            return Result<T>.Failure(result.Error);
        }
    }
}